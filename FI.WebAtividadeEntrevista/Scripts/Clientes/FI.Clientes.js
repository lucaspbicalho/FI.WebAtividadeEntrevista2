
$(document).ready(function () {

    $("#Beneficiarios").click(function () {
        ModalDialogBeneficiarios("Beneficiários");
    });

    $("#CPF").focus(function () {
        $("#CPF").inputmask("mask", { "mask": "999.999.999-99" }, { reverse: true });
    });
    
    $('#formCadastro').submit(function (e) {
        e.preventDefault();


        // se CPF igual a valido Envia requisição caso contrario exibe mensagem Digite um CPF válido
        var cpfValue = $("#CPF").val();
        cpfValue = cpfValue.replace('.', '').replace('-', '').replace('.', '');        
        if (validaCPF(cpfValue)) {
            $.ajax({
                url: urlPost,
                method: "POST",
                data: {
                    "NOME": $(this).find("#Nome").val(),
                    "CEP": $(this).find("#CEP").val(),
                    "CPF": $(this).find("#CPF").val(),
                    "Email": $(this).find("#Email").val(),
                    "Sobrenome": $(this).find("#Sobrenome").val(),
                    "Nacionalidade": $(this).find("#Nacionalidade").val(),
                    "Estado": $(this).find("#Estado").val(),
                    "Cidade": $(this).find("#Cidade").val(),
                    "Logradouro": $(this).find("#Logradouro").val(),
                    "Telefone": $(this).find("#Telefone").val()
                },
                error:
                    function (r) {
                        if (r.status == 400)
                            ModalDialog("Ocorreu um erro", r.responseJSON);
                        else if (r.status == 500)
                            ModalDialog("Ocorreu um erro", "Ocorreu um erro interno no servidor.");
                    },
                success:
                    function (r) {                                                                    
                      ModalDialog("Sucesso!", r);                      
                      var linhasTabela = $('#tableBeneficiario tr');
                      for (var i = 0; i < linhasTabela.length; i++) {
                        if (i == 0) {
                          continue;
                        }
                        $.ajax({
                          url: urlPostBeneficiario,
                          method: "POST",
                          data: {
                            "Id": $("#CPF").val(),
                            "NOME": linhasTabela[i].getElementsByTagName('td')[1].textContent,
                            "CPF": linhasTabela[i].getElementsByTagName('td')[0].textContent
                          },
                          error:
                            function (r) {
                              if (r.status == 400)
                                ModalDialog("Ocorreu um erro", r.responseJSON);
                              else if (r.status == 500)
                                ModalDialog("Ocorreu um erro", "Ocorreu um erro interno no servidor.");
                            },
                          success:
                            function (r) {

                            }
                        });
                      }
                      $("#formCadastro")[0].reset();
                    }
            });
        }
        else {
            ModalDialog("Ocorreu um erro", "Digite um CPF válido.");
        }
        
    })
    
})

function ModalDialog(titulo, texto) {
    var random = Math.random().toString().replace('.', '');
    var texto = '<div id="' + random + '" class="modal fade">                                                               ' +
        '        <div class="modal-dialog">                                                                                 ' +
        '            <div class="modal-content">                                                                            ' +
        '                <div class="modal-header">                                                                         ' +
        '                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>         ' +
        '                    <h4 class="modal-title">' + titulo + '</h4>                                                    ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-body">                                                                           ' +
        '                    <p>' + texto + '</p>                                                                           ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-footer">                                                                         ' +
        '                    <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>             ' +
        '                                                                                                                   ' +
        '                </div>                                                                                             ' +
        '            </div><!-- /.modal-content -->                                                                         ' +
        '  </div><!-- /.modal-dialog -->                                                                                    ' +
        '</div> <!-- /.modal -->                                                                                        ';

    $('body').append(texto);
    $('#' + random).modal('show');
}

function ModalDialogBeneficiarios(titulo) {
  var random = "modalBeneficiarios";
  if ($('#modalBeneficiarios').length == 0) {


    var texto = '<div id="' + random + '" class="modal fade">                                                               ' +
      '        <div class="modal-dialog">                                                                                 ' +
      '            <div class="modal-content">                                                                            ' +
      '                <div class="modal-header">                                                                         ' +
      '                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>         ' +
      '                    <h4 class="modal-title">' + titulo + '</h4>                                                    ' +
      '                </div>                                                                                             ' +



      '                <div class="row">                                                                                             ' +
      '                <div class="col-md-4" style="margin-left: 3%; margin-top: 4%;">                                                                                             ' +
      '                <div class="form-group">                                                                                             ' +
      '                <label for="CPF" style="display:inline">CPF:</label>                                                                                             ' +
      '                <input required="required" type="text" class="form-control" id="CPFBeneficiario" name="CPF" placeholder="" maxlength="14">                                                                                             ' +
      '                <label for="Nome" style="display:inline">Nome:</label>                                                                                             ' +
      '                <input required="required" type="text" class="form-control" id="NomeBeneficiario" name="Nome" placeholder="" maxlength="50">                                                                                             ' +
      '                <button type="button" id="buttonBeneficiario" onclick="buttonBeneficiario()" class="btn btn-sm btn-success">Incluir</button>                                                                                             ' +
      '                </div>                                                                                             ' +
      '                </div>                                                                                             ' +
      '                </div>                                                                                             ' +

      '                <table class="table" id="tableBeneficiario">                                                                                             ' +
      '                <tr>                                                                                             ' +
      '                <th>CPF</th>                                                                                             ' +
      '                <th>NOME</th>                                                                                             ' +
      '                <th></th>                                                                                             ' +
      '                <th></th>                                                                                             ' +
      '                </tr>                                                                                             ' +

      //'                </tr>                                                                                             ' +
      //'                <td>2323</td>                                                                                             ' +    
      //'                <td>2323</td>                                                                                             ' +
      //'                <td><button type="button" class="btn btn-sm btn-primary">Alterar</button></td>                     ' +
      //'                <td><button type="button" class="btn btn-sm btn-primary">Excluir</button></td>                     ' +
      //'                </tr>                                                                                             ' +

      '                </table>                                                                                             ' +





      '                <div class="modal-footer">                                                                         ' +
      '                    <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>             ' +
      '                                                                                                                   ' +
      '                </div>                                                                                             ' +
      '            </div><!-- /.modal-content -->                                                                         ' +
      '  </div><!-- /.modal-dialog -->                                                                                    ' +
      '</div> <!-- /.modal -->                                                                                        ';

    $('body').append(texto);
  }
  else {
    $('#CPFBeneficiario').val('');
    $('#NomeBeneficiario').val('');
  }
  $('#' + random).modal('show');
  $("#CPFBeneficiario").inputmask("mask", { "mask": "999.999.999-99" }, { reverse: true });

  $.ajax({
    url: urlPostGetBeneficiario,
    method: "POST",
    data: {
      "Id": $("#CPF").val(),
      "NOME": $("#NomeBeneficiario").val(),
      "CPF": $("#CPFBeneficiario").val()
    },
    error:
      function (r) {
        if (r.status == 400)
          ModalDialog("Ocorreu um erro", r.responseJSON);
        else if (r.status == 500)
          ModalDialog("Ocorreu um erro", "Ocorreu um erro interno no servidor.");
      },
    success:
      function (r) {
        for (var i = 0; i < r.length; i++) {
          $('#tableBeneficiario').append("<tr><td>" + r[i].CPF + "</td><td>" + r[i].Nome +
            "</td><td><button type='button' class='btn btn-sm btn-primary'>Alterar</button></td>" +
            "</td><td><button type='button' class='btn btn-sm btn-primary' onclick='removerBeneficiario(this)'>Excluir</button></td>" +
            "</tr > ");
        }
      }
  });
}

function validaCPF(strCPF) {
    var Soma;
    var Resto;
    Soma = 0;
    if (strCPF == "00000000000") return false;

    for (i = 1; i <= 9; i++) Soma = Soma + parseInt(strCPF.substring(i - 1, i)) * (11 - i);
    Resto = (Soma * 10) % 11;

    if ((Resto == 10) || (Resto == 11)) Resto = 0;
    if (Resto != parseInt(strCPF.substring(9, 10))) return false;

    Soma = 0;
    for (i = 1; i <= 10; i++) Soma = Soma + parseInt(strCPF.substring(i - 1, i)) * (12 - i);
    Resto = (Soma * 10) % 11;

    if ((Resto == 10) || (Resto == 11)) Resto = 0;
    if (Resto != parseInt(strCPF.substring(10, 11))) return false;
    return true;
  }

function buttonBeneficiario() {
  var cpfIgual = false;
  // se CPF igual a valido Envia requisição caso contrario exibe mensagem Digite um CPF válido
  var cpfValue = $("#CPFBeneficiario").val();
  cpfValue = cpfValue.replace('.', '').replace('-', '').replace('.', '');
  if ($("#NomeBeneficiario").val() == "") {
    ModalDialog("Ocorreu um erro", "Digite um Nome válido.");
    return;
  }
  $('#tableBeneficiario td').each(function () {
    var cellText = $(this).html();
    if (cellText == $("#CPFBeneficiario").val()) {

      cpfIgual = true;
    }

  });
  if (cpfIgual) {
    ModalDialog("Ocorreu um erro", "Digite um Novo CPF.");
    return;
  }
  if (validaCPF(cpfValue)) {

    $('#tableBeneficiario').append("<tr><td>" + $("#CPFBeneficiario").val() + "</td><td>" + $("#NomeBeneficiario").val() +
      "</td><td><button type='button' class='btn btn-sm btn-primary'>Alterar</button></td>" +
      "</td><td><button type='button' class='btn btn-sm btn-primary' onclick='removerBeneficiario(this)'>Excluir</button></td>" +
      "</tr > ");

  }
  else {
    ModalDialog("Ocorreu um erro", "Digite um CPF válido.");
  }

}

function removerBeneficiario(event) {
  event.parentElement.parentElement.remove();
}