angular.module('solicitacao-form', []);

var SolicitacaoFormCtrl = function ($scope, seed) {
  // Inicializar scope
  $scope = {};
  if (seed != null) {$scope = seed};
  
  $scope.Formatos = ["A4", "A3", "A5"];
  $scompe.Solicitacao.Formato = "A4";

  $scope.Suportes = ["E-Mail", "Zipdrive", "Papel", "CD"];
  $scope.Solicitacao.Suporte = "Papel"

    $scope.Areas = window.Areas;
    $scope.Fornecedores = window.Fornecedores;
    $scope.Codificacoes = window.Codificacoes;

  // Valor padrão: 11541 - 404503 : Educacao e Tecnologia - Especializacao Prof. Nivel Basico - Entidade
  $scope.Solicitacao.Codificacao = 23; 

  $scope.addNew = function () {
    $scope.Itens.push({});
  }
}