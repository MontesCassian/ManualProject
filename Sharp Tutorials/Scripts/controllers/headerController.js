tutorialsApp.controller('headerController', function ($scope) {

    $scope.SetActive = function (e) {
        var ul = angular.element(document.getElementById('header-nav')).children();
        for (var i = 0; i < ul.length; i++) {
            var aItem = angular.element(ul[i]);
            if (aItem.children().hasClass('active')) {
                aItem.children().removeClass('active');
            }
        }
        angular.element(e.target).addClass('active');
    }
});