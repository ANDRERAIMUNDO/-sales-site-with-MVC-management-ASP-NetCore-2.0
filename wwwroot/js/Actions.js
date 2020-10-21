$(document).ready(function () {
    $(".btn-danger").click(function (e){
        var resultado = confirm("deseja exlcluir ?");

        if (resultado == false) {
            e.preventDefault();
        }
    });
});