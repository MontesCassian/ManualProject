tutorialsApp.controller('sidebarController', function ($rootScope, $scope, $timeout, $http, $location, currentTab, currentPage, currentVideo) {

    $scope.idList = [];
    $scope.currentId = null;
    //CREATE MENU LIST AFTER LOADING PAGE
    $scope.CreateMenu = function () {

        var ul;
        var li;
        var a;
        var responseData;
        var request;
        //CHECK CURRENT PAGE TO CHOOSE SPECIAL CATEGORY OF MENU
        if (currentPage.get() == 3) {
            request = '/Tutorials/GetMenuList';
            angular.element(document.getElementById('SideBarTitle')).text('Уроки');
        } else if (currentPage.get() == 2) {
            request = '/Videos/GetMenuList';
            angular.element(document.getElementById('SideBarTitle')).text('Відео');

            var currVideo = currentVideo.get();
            if (currVideo != null) {
                $scope.ShowContent(currVideo);
            }
        }

        $http({ method: 'GET', url: request }).
            then(function success(response) {
                var httpResponse = response;

                console.log(httpResponse);
                responseData = httpResponse.data;

                for (var i = 0; i < responseData.length; i++) {

                    ul = angular.element(document.getElementById('MenuList'));
                    li = angular.element(document.createElement("li"));
                    a = angular.element(document.createElement("a"));
                    a.attr("href", "javascript:void(0)");
                    a.val(responseData[i]["Id"]);

                    $scope.idList.push(responseData[i]["Id"])

                    ul.append(li);
                    li.append(a);
                    a.on('click', function (e) {
                        $scope.ShowContent(e.target.value);
                        $scope.currentId = e.target.value;
                    });

                    if (currentPage.get() == 3) {
                        a.text(responseData[i]["MenuTitle"]);
                    } else if (currentPage.get() == 2) {
                        a.text(responseData[i]["Title"]);
                    }
                }
            });

        if (currentTab.get() != 'null') {
            $scope.ShowContent(currentTab.get());
        }
    }
    //LOAD CONTENT TO PAGE BY CLICKING TO BUTTON
    $scope.ShowContent = function (id) {
        $scope.currentId = id;
        angular.element(document.getElementById('ToolKit')).removeClass('collapse');
        var request;
        //CHECK PAGE TO CHOOSING PATH OF CONTENT
        if (currentPage.get() == 3) {
            request = '/Tutorials/GetContent';
        } else if (currentPage.get() == 2) {
            request = '/Videos/GetContent';
        }

        var cont = angular.element(document.getElementById('Content'));
        var title = angular.element(document.getElementById('ContTitle'));
        var loading = angular.element(document.getElementById('loading'));
        var responseData;
        return $http({ method: 'GET', url: request, params: { id: id } }).
            then(function success(response) {
                responseData = response.data;
                title.text(responseData[0]["Title"]);
                cont.html(responseData[0]["Text"]);
                angular.element(document.getElementById('Hometask')).text(responseData[0]["Hometask"]);
                currentTab.set(responseData[0]["Id"]);

                if (currentPage.get() == 3) {
                    AddLinkToVideo(responseData[0]["Id"]);
                } else if (currentPage.get() == 2) {
                    angular.element(document.getElementById('Iframe')).attr('src', 'https://www.youtube.com/embed/' + responseData[0]["VideoId"]).removeClass('collapse');
                }
            });
    }
    // ADD LINK TO RELATIVE VIDEO
    var AddLinkToVideo = function (id) {
        var responseData;
        var div = angular.element(document.getElementById('LinkVideo')).html('');
        return $http({ method: 'GET', url: '/Videos/GetTitleByTutorialId', params: { id: id } }).
            then(function succes(response) {
                responseData = response.data;

                for (var i = 0; i < responseData.length; i++) {
                    var p = angular.element(document.createElement('p'));
                    var a = angular.element(document.createElement('a')).text(responseData[i]['Title']).attr('href', 'javascript:void(0)').val(responseData[i]['Id']).
                        on('click', function (e) {
                            $scope.CreateMenu();
                            currentVideo.set(e.target.value);
                            $rootScope.$emit('CallSetVideoLink', {});
                            $location.path('/videos');
                        });
                    p.append(a);
                    div.append(p);
                    if (i == 0) {
                        angular.element(document.getElementById('Iframe')).attr('src', 'https://www.youtube.com/embed/' + responseData[0]["VideoId"]).removeClass('collapse');
                    }
                }
            });
    }
    //NAV BUTTON CONTROLLER
    var nextTab = function () {
        for (var i = 0; i < $scope.idList.length; i++) {

            if (($scope.idList[i] == $scope.currentId) && ((i + 1) != $scope.idList.length)) {
                $scope.ShowContent($scope.idList[i + 1]);
                $scope.currentId = $scope.idList[i + 1];
                return;
            }
        }
    }

    var prevTab = function () {
        for (var i = 0; i < $scope.idList.length; i++) {

            if (($scope.idList[i] == $scope.currentId) && ((i - 1) >= 0)) {
                $scope.ShowContent($scope.idList[i - 1]);
                $scope.currentId = $scope.idList[i - 1];
                return;
            }
        }
    }

    var OnCallNextTab = $rootScope.$on('CallNextTab', function () {
        nextTab();
    });

    var OnCallPrevTab = $rootScope.$on('CallPrevTab', function () {
        prevTab();
    });

    var OnCallFirstTab = $rootScope.$on('CallFirstTab', function() {
        $scope.ShowContent($scope.idList[0]);
    });

    $scope.$on('$destroy', function () {
        OnCallFirstTab();
        OnCallPrevTab();
        OnCallNextTab();
    });
    //------------------------------------------------------------
});
