var tutorialsApp = angular.module('tutorialsApp', ["ngRoute"])
    .config(function ($routeProvider) {
        $routeProvider.when('/home', {
            templateUrl: '../Templates/home.html'
        });
        $routeProvider.when('/videos', {
            templateUrl: '../Templates/videos.html'
        });
        $routeProvider.when('/tutorials', {
            templateUrl: '../Templates/tutorials.html'
        });
        $routeProvider.when('/editor', {
            templateUrl: '../Templates/editor.html'
        });
        $routeProvider.when('/test', {
            templateUrl: '../Templates/test.html'
        })
        $routeProvider.otherwise({ redirectTo: '/home' });
    })