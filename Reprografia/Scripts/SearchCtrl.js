function SearchCtrl ($scope) {
  $scope.solicitacoes = window.solicitacoes;
  $scope.filter = {};
  $scope.predicate = "+AnoSeq";
  $scope.advanced = false;
}