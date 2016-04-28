(function () {
    'use strict';

    angular
        .module('danterestapp')
        .controller('dansController', dansController);

    dansController.$inject = ['$scope', 'danfactory'];

    function dansController($scope, danfactory) {
        $scope.title = 'dansController';

        activate();

        $scope.savePin = function () {
            console.log('called controller savePin()');
            var newpin = { PhotoUrl: $scope.newPin.PhotoUrl, Description: $scope.newPin.Description, LinkUrl: $scope.newPin.LinkUrl };
            danfactory.savePin(newpin).then(function(res) {

                $scope.newPin.PhotoUrl = "";
                $scope.newPin.Description = "";
                $scope.newPin.LinkUrl = "";

                $scope.Pins.push(res.data);
            });
        }

        function activate() {
            danfactory.getAllPins().then(function (res) {
                console.log(res.data);
                $scope.Pins = res.data;
            });

           

        }
    }
})();
