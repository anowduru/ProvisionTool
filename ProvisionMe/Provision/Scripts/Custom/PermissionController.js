(function () {

    //var LandingPageController = function ($scope) {
    //    $scope.models = {
    //        helloAngular: 'I work!'
    //    };
    //}

    //// The $inject property of every controller (and pretty much every other type of object in Angular) needs to be a string array equal to the controllers arguments, only as strings
    //LandingPageController.$inject = ['$scope'];

    var module = angular.module("Permission", []);

    var _controller = function($scope)
    {
        $scope.Name = "Belton";
        //alert("test");
    }

    module.controller("PermissionController", _controller);

}());
