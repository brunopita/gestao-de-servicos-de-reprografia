angular.module('solicitacao-form', []);

var SolicitacaoFormCtrl = function ($scope) {
  // Inicializar scope
  $scope.Itens = [];

  $scope.Formatos = ["A4", "A3", "A5"];
  $scope.Suportes = ["E-Mail", "Zipdrive", "Papel", "CD"];

  //Padrões
  $scope.Solicitacao = {};
  $scope.Solicitacao.Formato = "A4";
  $scope.Solicitacao.Suporte = "Papel";
  $scope.Solicitacao.Area = "1";
  $scope.Solicitacao.Fornecedor = "1";

  //Pegar informações de seeding
  $scope.Areas = window.Areas;
  $scope.Fornecedores = window.Fornecedores;
  $scope.Codificacoes = window.Codificacoes;

  // Valor padrão: 11541 - 404503 : Educacao e Tecnologia - Especializacao Prof. Nivel Basico - Entidade
  $scope.Solicitacao.Codificacao = 23;

  $scope.addNew = function () {
    $scope.Itens.push({});
  };
};