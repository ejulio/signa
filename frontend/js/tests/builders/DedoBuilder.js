function DedoBuilder() {
    this._direcao = [0.0, 0.0, 0.0];
    this._posicaoDaPonta = [0.0, 0.0, 0.0];
    this._velocidadeDaPonta = [0.0, 0.0, 0.0];
    this._apontando = false;
    this._tipo = DedoBuilder.TipoDedo.DEDAO;
}
DedoBuilder.TipoDedo = {
    DEDAO: 0,
    INDICADOR: 1,
    MEIO: 2,
    ANELAR: 3,
    MINDINHO: 4
};

DedoBuilder.prototype = {
    _direcao: undefined,
    _tipo: DedoBuilder.TipoDedo.DEDAO,
    _apontando: false, 
    _posicaoDaPonta: undefined,
    _velocidadeDaPonta: undefined,

    comDirecao: function(direcao) {
        this._direcao = direcao;
        return this;
    },

    doTipo: function(tipo) {
        this._tipo = tipo;
        return this;
    },

    construir: function() {
        return {
            direction: this._direcao,
            type: this._tipo
        }
    }
};

function _dedoDoTipo(tipo) {
    var direcao = Math.random() * tipo; // direcao random
    return new DedoBuilder()
        .comDirecao([direcao, direcao, direcao])
        .doTipo(tipo)
        .construir();
}

DedoBuilder.dedao = function() { return _dedoDoTipo(DedoBuilder.TipoDedo.DEDAO); };
DedoBuilder.indicador = function() { return _dedoDoTipo(DedoBuilder.TipoDedo.INDICADOR); };
DedoBuilder.meio = function() { return _dedoDoTipo(DedoBuilder.TipoDedo.MEIO); };
DedoBuilder.anelar = function() { return _dedoDoTipo(DedoBuilder.TipoDedo.ANELAR); };
DedoBuilder.mindinho = function() { return _dedoDoTipo(DedoBuilder.TipoDedo.MINDINHO); };

module.exports = DedoBuilder;
