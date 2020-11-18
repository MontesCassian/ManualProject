tutorialsApp.controller('loginController', function ($scope, $http, $cookies, $location, $rootScope) {

    $scope.LogIn = function (login) {
        $http({ method: 'POST', url: '/Login/Login', data: login }).
            then(function succes(response) {
                var respData = response.data;

                if (respData == 1) {
                    $cookies.put('login', login.Login);
                    $cookies.put('password', login.Password);
                    $location.path('/editor');

                } else if (respData == 0) {
                    console.log('Login error');
                } else if (respData < 0) {
                    console.log('User db empty');
                }
            })
    }

    $rootScope.$on('CallLogin', function () {
        var login = {
            Login: $cookies.get('login'),
            Password: $cookies.get('password')
        }
        $scope.LogIn(login);
    })
});