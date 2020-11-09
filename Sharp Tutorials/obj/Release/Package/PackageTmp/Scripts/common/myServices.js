tutorialsApp.service('currentTab', function () {
    var currentId = {};

    this.set = function (tutorialId) {
        this.currentId = tutorialId;
    }

    this.get = function () {
        return (this.currentId);
    }
});

tutorialsApp.service('currentVideo', function () {
    var currentId = {};

    this.set = function (videolId) {
        this.currentId = videolId;
    }

    this.get = function () {
        var returned = this.currentId;
        this.currentId = null;
        return (returned);
    }
});

tutorialsApp.service('currentPage', function ($location) {
    var currentId = {}; //#!home=1, #!videos=2, #!tutorials=3

    this.set = function (tutorialId) {
        switch (tutorialId) {
            case '#!home': this.currentId = 1;
                break;
            case '#!videos': this.currentId = 2;
                break;
            case '#!tutorials': this.currentId = 3;
                break;
            default: this.currentId = 0;
        }
    }

    this.get = function () {
        switch ($location.path()) {
            case '/home': this.currentId = 1;
                break;
            case '/videos': this.currentId = 2;
                break;
            case '/tutorials': this.currentId = 3;
                break;
            default: this.currentId = 0;
        }
        return (this.currentId);
    }
})