$(function () {

    $("#select-programas").change(function () {
        var disabled = false;
        var selecao = $("#select-programas option:selected").val();
        if (selecao != undefined && selecao != null && selecao != "") {
            disabled = true;
        }
        $("#input-potencia").prop('disabled', disabled);
        $("#input-tempo").prop('disabled', disabled);
        $("#button-iniciar-rapido").prop('disabled', disabled);

        $("#input-potencia").val("");
        $("#input-tempo").val("");
    });

    $("#button-modal-programas").click(function () {
        $('#modal-programas-aquecimento').modal('show');
        $.ajax({
            url: "ProgramaAquecimento/ListaProgramaAquecimento/",
            data: { pesquisaAlimento: "" },
            type: "GET",
        })
        .done(function (data) {
            if (!data.hasOwnProperty("erro")) {
                $("#div-programas-painel").html(data);
            } else {
                alert(data.erro);
            }
        })
        .fail(function (data) {
            alert("Erro ao realizar o aquecimento");
        });
    });

    $("#button-programa-pesquisa").click(function () {
        var pesquisaAlimento = $("#input-pesquisa-alimento").val();

        $.ajax({
            url: "ProgramaAquecimento/ListaProgramaAquecimento/",
            data: { pesquisaAlimento: pesquisaAlimento },
            type: "GET",
            async: false
        })
        .done(function (data) {
            if (!data.hasOwnProperty("erro")) {
                $("#div-programas-painel").html(data);
            } else {
                alert(data.erro);
            }
        })
        .fail(function (data) {
            alert("Erro ao realizar o aquecimento");
        });
    });

    $("#button-programa-novo").click(function () {
        $.ajax({
            url: "ProgramaAquecimento/Create/",
            type: "GET",
        })
        .done(function (data) {
            if (!data.hasOwnProperty("erro")) {
                $("#div-programa-novo").html(data);
                $("#modal-programa-novo").modal("show");
            } else {
                alert(data.erro);
            }
        })
        .fail(function (data) {
            alert("Erro ao realizar o aquecimento");
        });
    });

    $("body").on("click", "#button-programa-gravar", function () {
        //Validar numeros
        var regra = /^[0-9]+$/;

        //Validações
        var nome = $("#input-create-nome").val();
        if (nome == "") {
            alert("Informe o nome");
            return;
        }
        var instrucoes = $("#textarea-create-instrucoes").val();
        if (instrucoes == "") {
            alert("Informe as instruções");
            return;
        }

        var potencia = $("#input-create-potencia").val();
        if (nome == "" || !potencia.match(regra)) {
            alert("Informe a potência em números");
            return;
        }

        var tempoSegundos = $("#input-create-tempo").val();
        if (nome == "" || !tempoSegundos.match(regra)) {
            alert("Informe o tempo em números");
            return;
        }
        var aquecimento = $("#input-create-aquecimento").val();
        if (aquecimento == "" || aquecimento.length > 1) {
            alert("Informe o caractere para aquecimento");
            return;
        }
        var alimentos = $("#input-create-alimentos").val();
        if (alimentos == "") {
            alert("Informe o alimento compatível");
            return;
        }

        $.ajax({
            url: "ProgramaAquecimento/Create/",
            type: "POST",
            data: { nome: nome, instrucoes: instrucoes, potencia: potencia, tempoSegundos: tempoSegundos, aquecimento: aquecimento, alimentos: alimentos }
        })
        .done(function (data) {
            if (!data.hasOwnProperty("erro")) {
                location.reload();
            } else {
                alert(data.erro);
            }
        })
        .fail(function (data) {
            alert("Erro ao adicionar o programa de aquecimento");
        });
    });
});