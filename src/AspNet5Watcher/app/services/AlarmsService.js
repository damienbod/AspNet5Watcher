(function () {
    'use strict';

	function AlarmsService($http, $log, $q) {
	    $log.info("alarmsService called");

	    var AddAlarm = function (alarm) {
	        var deferred = $q.defer();

	        console.log("addAlarm started");
	        console.log(alarm);

	        $http({
	            url: 'api/alarms/AddAlarm',
	            method: "POST",
	            data: alarm
	        }).success(function (data) {
	            deferred.resolve(data);
	        }).error(function (error) {
	            deferred.reject(error);
	        });
	        return deferred.promise;
	    };

		return {
		    AddAlarm: AddAlarm
		}
	}

	var module = angular.module('mainApp');

	// this code can be used with uglify
	module.factory("alarmsService",
		[
			"$http",
			"$log",
            "$q",
			AlarmsService
		]
	);

})();
