tutorialsApp.controller('headerController', function ($scope, $rootScope, currentPage, currentTab, $location) {
    //SETTING ACTIVE NAVBAR BUTTON
    $scope.SetActive = function (e) {
        var ul = angular.element(document.getElementById('header-nav')).children();
        //DEACTIVATE OTHER BUTTON 
        for (var i = 0; i < ul.length; i++) {
            var aItem = angular.element(ul[i]);
            if (aItem.children().hasClass('active')) {
                aItem.children().removeClass('active');
            }
        }
        //ACTIVATE CURRENT PUSHED BUTTON
        if (e != undefined) {
            var targ = angular.element(e.target);
            targ.addClass('active');
        }
        currentTab.set(null);
    }

    $scope.LogoClick = function () {
        $scope.SetActive();
        angular.element(document.getElementById('HomeLink')).addClass('active');
    }

    var SetActiveVideo = function () {       
        $scope.SetActive();
        angular.element(document.getElementById('VideoLink')).addClass('active');       
    }

    $rootScope.$on('CallSetVideoLink', function () {
        SetActiveVideo();
    });

});