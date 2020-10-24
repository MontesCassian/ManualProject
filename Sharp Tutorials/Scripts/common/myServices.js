tutorialsApp.service('currentTab', function () {
    var currentId = {};

    this.set = function (tutorialId) {
        this.currentId = tutorialId;
    }

    this.get = function () {
        return (this.currentId);
    }
})