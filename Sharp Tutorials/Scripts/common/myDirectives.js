tutorialsApp.directive("sideBar", function(){
    return {
        templateUrl: "../Templates/sidebarTemplate.html"
    }
})
    .directive('ngModelDynamic', ['$compile', function ($compile) {
        return {
            restrict: 'A',
            link: function (scope, element, attrs) {
                element.removeAttr('ng-model-dynamic');
                element.attr('ng-model', attrs.ngModelDynamic);
                $compile(element)(scope);
            }
        }
    }])