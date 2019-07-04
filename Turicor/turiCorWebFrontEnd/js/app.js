﻿myApp = angular.module('myApp', []);

myApp.controller('reservarVehiculosController', function ($scope, $http) {
    $scope.paises = [];
    $scope.ciudadesPais = [];
    $scope.lugares = [{ 'Id': '1', 'Nombre': 'Aeropuerto' }, { 'Id': '2', 'Nombre': 'TerminalBuses' }, { 'Id': '3', 'Nombre': 'Hotel' }]
    $scope.vehiculos = [];
    $http({
        method: 'GET',
        url: 'http://localhost:50246/api/paises',
        Headers : {
            'content-type' : 'application/json; charset=utf-8'
        }
    }).then(function (response) {
        $scope.paises = response.data.$values;
        })

    $scope.buscarCiudades = function (idPais) {
        $http({
            method: 'GET',
            url: 'http://localhost:50246/api/ciudades/' + idPais, 
            Headers: {
                'content-type' : 'application/json; charset=utf-8'
            }
        }).then(function (response) {
            $scope.ciudadesPais = response.data.$values;
        })
    }

    $scope.buscarVehiculos = function (idCiudad, fechaRetiro, fechaDevolucion) {
        $http({
            method: 'GET',
            url: 'http://localhost:50246/api/vehiculos' +'?idCiudad='+idCiudad+'&fechaRetiro='+fechaRetiro+'&fechaDevolucion='+fechaDevolucion,
        }).then(function (response) {
            $scope.vehiculos = response.data.$values;
        })
    }

    $scope.printVehiculo = function (vehiculo) {
        console.log(vehiculo);

    }





})


/*myApp = angular.module('myApp', []);
myApp.controller('VehiculosDispController',
    function ($scope) {
        $scope.Titulo = "View: Vehículos Disponibles";

        $scope.IdPais = null;
        $scope.IdCiudad = null;
        $scope.ListaVehiculos = [{ Id: 'Jorge', Marca: "CHEVROLET", Modelo: "CORSA", Puntaje = 5, Puertas = 3, Disponibles = 1, Aire = "N", Direccion = "Asistida", Cambio = "Manual" },
        { Id: 'Jorge', Marca: "PEUGEOT", Modelo: "208 FELINE", Puntaje = 5, Puertas = 5, Disponibles = 3, Aire = "S", Direccion = "Electrica", Cambio = "Manual"},
        { Id: 'Jorge', Marca: "PEUGEOT", Modelo: "308", Puntaje = 5, Puertas = 5, Disponibles = 7, Aire = "S", Direccion = "Electrica", Cambio = "Automatico" }];

        $scope.Personas =
            [{ nombre: 'Juana', edad: 82 },
            { nombre: 'Jorge', edad: 30 },
            { nombre: 'Mario', edad: 13 },
            { nombre: 'Javier', edad: 43 },
            { nombre: 'Carlos', edad: 19 },
            { nombre: 'Luis', edad: 47 }];

    }

        //$http.get('/api/ArticulosFamilias')
          //  .then(function (response) {
            //    $scope.Lista = response.data;
            //});
    });

myApp.controller('reservarVehiculoController',
    function ($scope, $http) {
        $scope.Titulo = "Viva la pocha";
    });


myApp.controller("myCtrl",
function ($scope) {
    $scope.Titulo = "Viva la fafa";
    $scope.Contador = 0;
    $scope.incrementar = function () { $scope.Contador++; };

    $scope.FiltroPersonas = 'j';
    $scope.personas =
        [{ nombre: 'Juana', edad: 82 },
        { nombre: 'Jorge', edad: 30 },
        { nombre: 'Mario', edad: 13 },
        { nombre: 'Javier', edad: 43 },
        { nombre: 'Carlos', edad: 19 },
        { nombre: 'Luis', edad: 47 }];
    }
);
*/