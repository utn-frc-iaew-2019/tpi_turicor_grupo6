myApp = angular.module('myApp', []);

myApp.controller('reservarVehiculosController', function ($scope, $http) {
    $scope.paises = [];
    $scope.ciudadesPais = [];
    $scope.lugares = [{ 'Id': '1', 'Nombre': 'Aeropuerto' }, { 'Id': '2', 'Nombre': 'Terminal' }, { 'Id': '3', 'Nombre': 'Hotel' }];
    $scope.vehiculos = [];
    $scope.msg = "Naranja...";
    $scope.vehiculoSelected = {};
    $scope.vendedores = [{ 'Id': '1', 'Nombre': 'Gonzalo' }, { 'Id': '2', 'Nombre': 'Roberto' }, { 'Id': '3', 'Nombre': 'Roman'}];

    $http({
        method: 'GET',
        url: 'http://localhost:50246/api/paises',
        Headers: {
            'content-type': 'application/json; charset=utf-8'
        }
    }).then(function (response) {
        $scope.paises = response.data.$values;
    });

    $scope.buscarCiudades = function (idPais) {
        $http({
            method: 'GET',
            url: 'http://localhost:50246/api/ciudades/' + idPais,
            Headers: {
                'content-type': 'application/json; charset=utf-8'
            }
        }).then(function (response) {
            $scope.ciudadesPais = response.data.$values;
        });
    };

    $scope.buscarVehiculos = function (idCiudad, fechaRetiro, fechaDevolucion) {
        $http({
            method: 'GET',
            url: 'http://localhost:50246/api/vehiculos' + '?idCiudad=' + idCiudad + '&fechaRetiro=' + fechaRetiro + '&fechaDevolucion=' + fechaDevolucion
        }).then(function (response) {
            $scope.vehiculos = response.data.$values;
        });
    };

    $scope.reservarVehiculo = function (fechaRetiro, fechaDevolucion, lugarRetiroSelected, lugarDevolucionSelected, userName, userApe, userDni,
        vendedorSelected, paisSelected, ciudadSelected, vehiculoSelected) {
        var fecha1 = moment(fechaRetiro);
        var fecha2 = moment(fechaDevolucion);
        var dif = fecha2.diff(fecha1)
        var totalReserva = dif * vehiculoSelected.PrecioPorDia * 1.20;
        var userNombre = userApe + ', ' + userName;

        //Generamos un objeto json con características similares a ReservaEntity
        var reservaJson = {
            'NroDocumentoCliente': userDni,
            'ApellidoNombreCliente': userNombre,
            'FechaHoraDevolucion': fechaDevolucion,
            'FechaHoraRetiro': fechaRetiro,
            'LugarDevolucion': lugarDevolucionSelected.Nombre,
            'LugarRetiro': lugarRetiroSelected.Nombre,
            'VehiculoPorCiudadId': vehiculoSelected.VehiculoCiudadId,
            'TotalReserva': totalReserva
        };
        console.log(reservaJson);
        var idCiudad = ciudadSelected.Id;
        var idPais = paisSelected.Id;
        var idVendedor = vendedorSelected.Id;
        $http.post('http://localhost:50246/api/reservas?idCiudad=' + idCiudad + '&idPais=' + idPais + '&idVendedor=' + idVendedor, reservaJson);
    }
});

myApp.controller('consultaReservasView', function ($scope, $http) {
    $scope.reservas = [];
    $http({
        method: 'GET',
        url: 'http://localhost:50246/api/reservas',
        Headers: {
            'content-type': 'application/json; charset=utf-8'
        }
    }).then(function (response) {
        $scope.reservas = response.data.$values;
    });
    }
);

