/// <reference path="../react-0.13.0.js" />
/** @jsx React.DOM */

var SAMPLE_DATA = [
  {"unread": false, "desc": "Hey, I just wanted to check in with you from Toronto. I got here earlier today.", "name": "Tilo Mitra", "avatar": "http://api.twitter.com/1/users/profile_image?screen_name=tilomitra&amp;size=bigger", "subject": "Hello from Toronto", "timestamp": "3:56pm, April 3, 2012", "content": "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat."},
  {"unread": true, "desc": "Hey, I had some feedback for pull request #51. We should center the menu so it looks better on mobile.", "name": "Eric Ferraiuolo", "avatar": "http://api.twitter.com/1/users/profile_image?screen_name=yuilibrary&amp;size=bigger", "subject": "Re: Pull Requests", "timestamp": "2:27pm, April 3, 2012", "content": "Duis aute irure dolor in reprehenderit in voluptate velit essecillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."},
  {"unread": true, "desc": "Duis aute irure dolor in reprehenderit in voluptate velit essecillum dolore eu fugiat nulla.", "name": "YUI Library", "avatar": "http://api.twitter.com/1/users/profile_image?screen_name=reid&amp;size=bigger", "subject": "You have 5 bugs assigned to you", "timestamp": "12:05am, April 2, 2012", "content": "Aliquam ac feugiat dolor. Proin mattis massa sit amet enim iaculis tincidunt. Mauris tempor mi vitae sem aliquet pharetra. Fusce in dui purus, nec malesuada mauris. Curabitur ornare arcu quis mi blandit laoreet. Vivamus imperdiet fermentum mauris, ac posuere urna tempor at. Duis pellentesque justo ac sapien aliquet egestas. Morbi enim mi, porta eget ullamcorper at, pharetra id lorem."},
  {"unread": false, "desc": "Excepteur sint occaecat cupidatat non proident, sunt in culpa.", "name": "Reid Burke", "avatar": "http://api.twitter.com/1/users/profile_image?screen_name=triptych&amp;size=bigger", "subject": "Re: Design Language", "timestamp": "1:00pm, April 2, 2012", "content": "Donec sagittis dolor ut quam pharetra pretium varius in nibh. Suspendisse potenti. Donec imperdiet, velit vel adipiscing bibendum, leo eros tristique augue, eu rutrum lacus sapien vel quam. Nam orci arcu, luctus quis vestibulum ut, ullamcorper ut enim. Morbi semper erat quis orci aliquet condimentum. Nam interdum mauris sed massa dignissim rhoncus."},
  {"unread": false, "desc": "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip.", "name": "Andrew Wooldridge", "avatar": "http://api.twitter.com/1/users/profile_image?screen_name=yahoofinance&amp;size=bigger", "subject": "YUI Blog Updates?", "timestamp": "12:59pm, April 2, 2012", "content": "Nevermind, I got it."},
  {"unread": false, "desc": "Mauris tempor mi vitae sem aliquet pharetra. Fusce in dui purus, nec malesuada mauris.", "name": "Yahoo! Finance", "avatar": "http://api.twitter.com/1/users/profile_image?screen_name=yahoonews&amp;size=bigger", "subject": "How to protect your finances from winter storms", "timestamp": "12:00pm, April 2, 2012", "content": "I ran out of Lorem Ipsum!"}
];

var Nav = React.createClass({
    render: function() {
        return (
          <div className="pure-u id-nav">
              <a href="#nav" className="nav-menu-button">Menu</a>

              <div className="nav-inner">
                  <a className="pure-button primary-button" href="#">Compose</a>

                  <div className="pure-menu pure-menu-open">
                      <ul>
                          <li><a href="#">Inbox <span className="email-count">(2)</span></a></li>
                          <li><a href="#">Important</a></li>
                          <li><a href="#">Sent</a></li>
                          <li><a href="#">Drafts</a></li>
                          <li><a href="#">Trash</a></li>
                          <li className="pure-menu-heading">Labels</li>
                          <li><a href="#"><span className="email-label-personal"></span>Personal</a></li>
                          <li><a href="#"><span className="email-label-work"></span>Work</a></li>
                          <li><a href="#"><span className="email-label-travel"></span>Travel</a></li>
                      </ul>
                  </div>
              </div>
          </div>
      );
    }
});

var EmailItem = React.createClass({
    render: function() {
        var classes = 'email-item pure-g';
        if (this.props.selected) {
            classes += ' email-item-selected';
        }
        if (this.props.unread) {
            classes += ' email-item-unread';
        }
        return (
          <div className={classes}>
          <div className="pure-u">
          <img className="email-avatar" alt={this.props.name + '\'s avatar'} src={this.props.avatar} height="65" width="65"/>
        </div>

        <div class="pure-u-3-4">
          <h5 className="email-name">{this.props.name}</h5>
          <h4 className="email-subject">{this.props.subject}</h4>
          <p className="email-desc">
            {this.props.children}
          </p>
        </div>
      </div>
    );
}
});

var List = React.createClass({
    render: function() {
        var items = this.props.emails.map(function(email, i) {
            return (
              <EmailItem
                  onClick={this.props.onEmailSelected.bind(this.props, i)}
                avatar={email.avatar}
            selected={this.props.selected === i}
            name={email.name}
            unread={email.unread && !this.props.read[i]}
subject={email.subject}>
{email.desc}
</EmailItem>
      );
}.bind(this));

return (
  <div class="pure-u id-list">
      <div class="content">
         {items}
      </div>
  </div>
    );
}
});

var Main = React.createClass({
    render: function() {
        return (
          <div className="pure-u id-main">
              <div className="content">
                  <div className="email-content pure-g">
                      <div className="email-content-header pure-g">
                          <div className="pure-u-1-2">
                              <h1 className="email-content-title">{this.props.email.subject}</h1>
                              <p className="email-content-subtitle">
                                  From <a>{this.props.email.name}</a> at <span>{this.props.email.timestamp}</span>
                              </p>
                          </div>

                          <div className="pure-u-1-2 email-content-controls">
                              <a className="pure-button secondary-button">Reply</a>
                              <a className="pure-button secondary-button">Forward</a>
                              <a className="pure-button secondary-button">Move to</a>
                          </div>
                      </div>

                      <div className="email-content-body pure-u-1" dangerouslySetInnerHTML={{__html: this.props.email.content}} />
                </div>
            </div>
        </div>
    );
}
});

var App = React.createClass({
    getInitialState: function() {
        return {selected: 0, read: {}};
    },
    handleEmailSelected: function(index) {
        var read = this.state.read;
        read[this.state.selected] = true;
        this.setState({selected: index, read: read});
    },
    render: function() {
        return (
          <div className="pure-g-r content id-layout">
            <Nav />
            <List emails={this.props.emails} selected={this.state.selected} onEmailSelected={this.handleEmailSelected} read={this.state.read} />
            <Main email={this.props.emails[this.state.selected]} />
          </div>
      );
    }
});

$.getJSON('/home/emailjson', function(emails) {    
    React.render(<App emails={emails} />, document.getElementById('emailApp'));
});