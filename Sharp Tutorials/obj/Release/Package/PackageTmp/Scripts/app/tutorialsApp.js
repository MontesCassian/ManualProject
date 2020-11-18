var tutorialsApp = angular.module('tutorialsApp', ["ngRoute", "ngCookies"])
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
        $routeProvider.when('/login', {
            templateUrl: '../Templates/login.html'
        })
        $routeProvider.otherwise({ redirectTo: '/home' });
    }).
    run(function ($rootScope, $cookies) {
        $rootScope.$on('$routeChangeStart', function (next, current) {
            if (current.$$route.originalPath == '/login') {
                if ($cookies.get('password') == undefined) {
                    console.log('login');
                    console.log('current rout: ' + JSON.stringify(current));
                } else {
                    $rootScope.$emit('CallLogin', {});
                }
                console.log($cookies.get('password'));
            }
        });
    });