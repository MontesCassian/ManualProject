tutorialsApp.controller('tutorialController', function ($scope, $http, $location, currentTab) {

    $scope.goToTest = function () {
        console.log("data from service "+currentTab.get());
        $location.path('/test');
    }
})