tutorialsApp.controller('tutorialController', function ($rootScope, $scope, $http, $location, currentTab) {

    $scope.goToTest = function () {
        console.log("data from service "+currentTab.get());
        $location.path('/test');
    }

    $scope.PrevTab = function () {
        $rootScope.$emit('CallPrevTab', {});
    }

    $scope.NextTab = function () {
        $rootScope.$emit('CallNextTab', {});
    }

    $scope.GoToTab = function () {
        $rootScope.$emit('CallFirstTab', {});
    }
})