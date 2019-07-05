myApp = angular.module('myApp', []);

myApp.controller('reservarVehiculosController', function ($scope, $http) {
    $scope.paises = [];
    $scope.ciudadesPais = [];
    $scope.lugares = [{ 'Id': '1', 'Nombre': 'Aeropuerto' }, { 'Id': '2', 'Nombre': 'Terminal' }, { 'Id': '3', 'Nombre': 'Hotel' }];
    $scope.vehiculos = [];
    $scope.vehSelected = [];
    $scope.json = [];
    $scope.params = [];
    $scope.msg = "Naranja...";
    $scope.vehiculoSelected = {};
    $scope.vendedores = [{ 'Id': '1', 'Nombre': 'Gonzalo' }, { 'Id': '2', 'Nombre': 'Roberto' }, { 'Id': '3', 'Nombre': 'Roman' }];

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
        vendorSelected, paisSelected, ciudadSelected, vehiculoSelected) {
        $scope.vehSelected = vehiculoSelected;
        let fecha1 = new Date(fechaRetiro);
        let fecha2 = new Date(fechaDevolucion);
        let difTime = fecha2.getTime() - fecha1.getTime();
        var dif = Math.round(difTime / (1000 * 60 * 60 * 24));

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

        $scope.json = reservaJson;

        var idCiudad = ciudadSelected.Id;
        var idPais = paisSelected.Id;
        var idVendedor = vendorSelected;

        var params = {
            'idCiudad': idCiudad,
            'idPais': idPais,
            'idVendedor': idVendedor
        };

        $scope.params = params;

        window.alert("Su reserva se ha efectuado correctamente...");

        $http.post('http://localhost:50246/api/reservas?idCiudad=' + idCiudad + '&idPais=' + idPais + '&idVendedor=' + idVendedor, reservaJson);
    };
});

myApp.controller('buscarReservas', function ($scope, $http) {
    $scope.pressed = false;

    $scope.apenom = "Guido Herrera";

    $scope.reserves = [
        { 'Cliente': 'Guido Herrera', 'Dni': '37450231', 'FechaRetiro': '2017-05-01', 'LugarRetiro': 'Aeropuerto', 'FechaDev': '2017-05-06', 'LugarDev': 'Terminal', 'IdVehCiud': '17' },
        { 'Cliente': 'Guido Herrera', 'Dni': '37450231', 'FechaRetiro': '2017-05-15', 'LugarRetiro': 'Aeropuerto', 'FechaDev': '2017-06-01', 'LugarDev': 'Hotel', 'IdVehCiud': '58' },
        { 'Cliente': 'Guido Herrera', 'Dni': '37450231', 'FechaRetiro': '2017-06-01', 'LugarRetiro': 'Terminal', 'FechaDev': '2017-06-08', 'LugarDev': 'Aeropuerto', 'IdVehCiud': '10' }
    ];

    var dni = $scope.userDni;

    $scope.reservarVehiculo = function (dni) {
        for (r in reserves) {
            if (r.Dni === dni) {
                $scope.apenom = r.Cliente;
            }
        }
    };
}
);

myApp.controller('verReservas', function ($scope, $http) {
    $scope.pressed = false;
    $scope.reservas = [
        { 'Cliente': 'Guido Herrera', 'Dni': '37450231', 'FechaRetiro': '2017-05-01', 'LugarRetiro': 'Aeropuerto', 'FechaDev': '2017-05-06', 'LugarDev': 'Terminal', 'IdVehCiud': '17' },
        { 'Cliente': 'Guido Herrera', 'Dni': '37450231', 'FechaRetiro': '2017-05-15', 'LugarRetiro': 'Aeropuerto', 'FechaDev': '2017-06-01', 'LugarDev': 'Hotel', 'IdVehCiud': '58' },
        { 'Cliente': 'Guido Herrera', 'Dni': '37450231', 'FechaRetiro': '2017-06-01', 'LugarRetiro': 'Terminal', 'FechaDev': '2017-06-08', 'LugarDev': 'Aeropuerto', 'IdVehCiud': '10' }
    ];

}
);