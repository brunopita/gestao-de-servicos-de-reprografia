var searchModule = angular.module('search', []);
searchModule.filter('bool', function () {
  return function (bool) {
    return bool ? "Sim" : "Não";
  }
})