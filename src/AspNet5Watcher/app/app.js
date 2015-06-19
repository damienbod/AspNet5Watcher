(function () {
	var mainApp = angular.module("mainApp", ["ui.router"]);

	mainApp.config(["$stateProvider", "$urlRouterProvider",
		function ($stateProvider, $urlRouterProvider) {
            	$urlRouterProvider.otherwise("/home/overview");

            	$stateProvider
                    .state("home", { abstract: true, url: "/home", templateUrl: "/templates/home.html" })
                        .state("overview", {
                        	parent: "home", url: "/overview", templateUrl: "/templates/overview.html", controller: "overviewController",
                        	
                        })
        }
	]
    );

	
})();
