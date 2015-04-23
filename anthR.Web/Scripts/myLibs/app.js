/// <reference path="notifyR.js" />
/// <reference path="../jquery.signalR-2.2.0.js" />

var notifyR = new window.NotifyR({ desktopNotifications: true, browserNotifications: true });

var connection = $.connection;
var hubService = connection.notificationHub;

var sendMessage = function (msg, to) {
    hubService.server.message({ msg: msg, user: to });
};

$(function () {    
        
    hubService.client.message = function (data) {
        if (data.user.toLowerCase() === _username) {
            notifyR.message(data.msg);
        }        
    };

    connection.hub.start().done(function () {
        //notifyR.info("connection started");
    });

    console.log('*********************************************************************************************');
    console.log('             _   _    ______ ');
    console.log('            | | | |   | ___ \\');    
    console.log('  __ _ _ __ | |_| |__ | |_/ /');
    console.log(" / _` | '_ \\| __| '_ \\|    / ");
    console.log("| (_| | | | | |_| | | | |\\ \\ ");
    console.log(" \\__,_|_| |_|\\__|_| |_\\_| \\_|");
    console.log('');
    console.log('*********************************************************************************************');
    console.log('o hai!');
    console.log('welcome to the console.');
    console.log('see if someone is online by bugging them with sendMessage("message", "username");');
    console.log('have fun, and behave!');
    console.log('*********************************************************************************************');



});