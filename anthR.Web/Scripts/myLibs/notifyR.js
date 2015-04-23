(function () {

    var NotifiR = function (options) {

        var self = this;
        var defaults = {
            desktopNotifications: false,
            browserNotifications: true
        }

        $.extend(defaults, options);

        self.desktopNotifications = defaults.desktopNotifications;
        self.browserNotifications = defaults.browserNotifications;

        self.messageTimeout = 4000;

        self.types = {
            normal: 0,
            info: 1,
            warning: 2,
            alert: 3,
            error: 4,
            message: 5
        };

        self.notify = function (msg) {
            self.displayNotification(msg, self.types.normal);
        };

        self.alert = function (msg) {
            self.displayNotification(msg, self.types.alert);
        };

        self.info = function (msg) {
            self.displayNotification(msg, self.types.info);
        };

        self.warning = function (msg) {
            self.displayNotification(msg, self.types.warning);
        };

        self.error = function (msg) {
            self.displayNotification(msg, self.types.error);
        };

        self.message = function (msg) {
            self.displayNotification(msg, self.types.message);
        }

        self.displayNotification = function (msg, type) {
            /// <summary>Displays a notification message on the screen</summary>

            if (self.browserNotifications) {

                // check for notifiR elements
                var $notifiR = $('#notifiR');
                if ($notifiR.length === 0) {
                    $notifiR = $('<ul />', { attr: { id: 'notifiR' } });
                    $('body').append($notifiR);
                }

                var cssClass = '';

                switch (type) {
                    case self.types.normal:
                        cssClass = 'nfr-n';
                        break;
                    case self.types.info:
                        cssClass = 'nfr-i';
                        break;
                    case self.types.message:
                        cssClass = 'nfr-i';
                        break;
                    case self.types.warning:
                        cssClass = 'nfr-w';
                        break;
                    case self.types.alert:
                        cssClass = 'nfr-a';
                        break;
                    case self.types.error:
                        cssClass = 'nfr-e';
                        break;
                    default:
                        cssClass = '';
                        break;
                }

                // create our notification element
                var $notifiRMsg = $('<li />', { html: msg }).click(function () { $(this).remove(); }).addClass(cssClass);

                // append notification element to $notifiR
                $notifiR.append($notifiRMsg);

                // set a rudamental timeout to remove the notification message after n seconds
                setTimeout(function () {
                    $notifiRMsg.remove();
                }, self.messageTimeout);

            }

            if (self.desktopNotifications) {

                // check if the browser supports notifications
                if (!("Notification" in window)) {
                    alert("This browser does not support desktop notifications");
                }
                    // check if the user is ok to get some notification
                else if (Notification.permission === 'granted') {

                    var title = "";

                    switch (type) {
                        case self.types.normal:
                            title = "Message";
                            break;
                        case self.types.info:
                            title = "Info";
                            break;
                        case self.types.warning:
                            title = "Warning";
                            break;
                        case self.types.alert:
                            title = "Alert!";
                            break;
                        case self.types.error:
                            title = "Error";
                            break;
                        case self.types.message:
                            title = "Message";
                            break;
                        default:
                            cssClass = '';
                            break;
                    }

                    var notification = new Notification(title, {
                        icon: '/content/livery/icon_64.png',
                        body: msg
                    });
                    notification.onclick = function () {
                        window.location = "/tasks";
                    };
                }
                    // otherwise we need to ask the user for permission
                else if (Notification.permission !== 'denied') {
                    Notification.requestPermission(function (permission) {
                        if (permission === "granted") {
                            self.info();
                        }
                    });
                }

            }

        };

        self.toolTip = function () {

        };

        return {
            alert: self.alert,
            info: self.info,
            warning: self.warning,
            error: self.error,
            notify: self.notify,
            message: self.message
        }

    };

    window.NotifyR = NotifiR;

})($);