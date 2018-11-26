$(function () {
    $("#button-iniciar").click(function () {
        desabilitarPausaECancelar(false);

        var caminho = "MicroOndas/";
        var data = {};
        data.conteudo = $("#textarea-conteudo-micro-ondas").val();
        data.codigo = $("#codigo-micro-ondas").val();

        var programaNome = $("#select-programas option:selected").val();

        if (programaNome == "") {
            data.potencia = $("#input-potencia").val();
            data.tempoSegundos = $("#input-tempo").val();
            caminho += "IniciarAquecimento/";
        } else {
            data.programaNome = programaNome;
            caminho += "IniciarAquecimentoPrograma/";
        }

        $.ajax({
            url: caminho,
            type: "GET",
            contentType: "application/json",
            data: data
        })
        .done(function (data) {
            if (!data.hasOwnProperty("erro")) {
                $("#textarea-conteudo-micro-ondas").val(data.conteudo);
                if(data.terminou)
                    alert("Aquecimento concluído");
                    desabilitarPausaECancelar(true);
            } else {
                alert(data.erro);
                desabilitarPausaECancelar(true);
            }
        })
        .fail(function (data) {
            alert("Erro ao realizar o aquecimento");
            desabilitarPausaECancelar(true);
        });
        
    });

    $("#button-iniciar-rapido").click(function () {
        desabilitarPausaECancelar(false);

        var conteudo = $("#textarea-conteudo-micro-ondas").val();
        var codigo = $("#codigo-micro-ondas").val();

        $.ajax({
            url: "MicroOndas/IniciarAquecimentoRapido/",
            data: { conteudo: conteudo, codigo: codigo },
            type: "GET",
        })
        .done(function (data) {
            if(!data.hasOwnProperty("erro"))
            {
                $("#textarea-conteudo-micro-ondas").val(data.conteudo);
                if (data.terminou)
                    alert("Aquecimento concluído");
                    desabilitarPausaECancelar(true);
            }else {
                alert(data.erro);
                desabilitarPausaECancelar(true);
            }
        })
        .fail(function (data) {
            alert("Erro ao realizar o aquecimento");
            desabilitarPausaECancelar(true);
        });
    });

    $("#button-pausa").click(function () {

        var codigo = $("#codigo-micro-ondas").val();

        $.ajax({
            url: "MicroOndas/Pausar/",
            data: { codigo: codigo },
            type: "GET",
        })
        .done(function (data) {
            if (!data.hasOwnProperty("erro")) {
                $("#textarea-conteudo-micro-ondas").val(data.conteudo);
                alert("Pausado, para retomar clique em iniciar.");
            } else {
                alert(data.erro);
            }
        })
        .fail(function (data) {
            alert("Erro ao realizar a pausa");
        });
    });


    $("#button-cancelar").click(function () {

        var codigo = $("#codigo-micro-ondas").val();

        $.ajax({
            url: "MicroOndas/Cancelar/",
            data: { codigo: codigo },
            type: "GET",
        })
        .done(function (data) {
            if (!data.hasOwnProperty("erro")) {
                $("#textarea-conteudo-micro-ondas").val(data.conteudo);
                alert("Cancelado.");
            } else {
                alert(data.erro);
            }
        })
        .fail(function (data) {
            alert("Erro ao realizar a pausa");
        });
    });
});

function desabilitarPausaECancelar(valor) {
    $("#button-pausa").prop('disabled', valor);
    $("#button-cancelar").prop('disabled', valor);
}