(function () {
	'use strict';

	function AlarmsService($http, $log, $q) {

	    $log.info("alarmsService called");

	    var addAlarm = function (alarm) {
	        var deferred = $q.defer();
	        $http({
	            url: 'api/alarms/addAlarm',
	            method: "POST",
	            data: project
	        }).success(function (data) {
	            deferred.resolve(data);
	        }).error(function (error) {
	            deferred.reject(error);
	        });
	        return deferred.promise;
	    };

		return {
		    addAlarm: addAlarm
		}
	}

	var module = angular.module('mainApp');

	// this code can be used with uglify
	module.factory("alarmsService",
		[
			"$http",
			"$log",
			AlarmsService
		]
	);

})();
