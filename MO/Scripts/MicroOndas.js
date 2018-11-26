$(function () {
    $("#button-iniciar").click(function () {

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
            } else {
                alert(data.erro);
            }
        })
        .fail(function (data) {
            alert("Erro ao realizar o aquecimento");
        });
        
    });

    $("#button-iniciar-rapido").click(function () {

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
            }else {
                alert(data.erro);
            }
        })
        .fail(function (data) {
            alert("Erro ao realizar o aquecimento");
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