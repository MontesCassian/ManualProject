var tutorialsApp = angular.module('tutorialsApp', ["ngRoute"])
.config(function($routeProvider){
    $routeProvider.when('/home',{
        templateUrl:'views/home.html'
    });
    $routeProvider.when('/videos',{
        templateUrl:'views/videos.html'
    });
    $routeProvider.when('/tutorials',{
        templateUrl:'views/tutorials.html'
    });
    $routeProvider.otherwise({redirectTo: '/home'});
})