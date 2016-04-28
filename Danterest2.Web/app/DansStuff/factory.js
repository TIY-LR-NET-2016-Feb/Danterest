(function () {
    'use strict';

    angular
        .module('danterestapp')
        .factory('danfactory', factory);

    factory.$inject = ['$http'];

    function factory($http) {
        var service = {
            getAllPins: getAllPins,
            savePin: savePin
        };

        return service;

        function getAllPins() {
            return $http.get('/pins/GetAllMyPins');

        }

        function savePin(newpin) {
            console.log('factory saved pin')
            return $http.post('/pins/SavePin', newpin);

        }
    }
})();