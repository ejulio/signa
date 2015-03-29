;(function(window, Signa, undefined) {
    'use strict';

    function ReconhecedorDeSinaisOnline() {
        this._informacoesDoFrame = new Signa.frames.InformacoesDoFrame();
    }

    ReconhecedorDeSinaisOnline.prototype = {
        _informacoesDoFrame: undefined,
        _idDoSinalParaReconhecer: -1,
        _algoritmo: undefined,
        _tipoDoSinal: -1,

        getIdDoSinal: function() {
            return this._idDoSinalParaReconhecer;
        },

        getTipoDoSinal: function() {
            return this._tipoDoSinal;
        },

        reconhecer: function(frame) {
            if (this._idDoSinalParaReconhecer === -1)
                return Promise.resolve(false);

            var dados = this._informacoesDoFrame.extrairParaAmostra(frame);
            return this._algoritmo
                .reconhecer(dados)
                .then(function(sinalFoiReconhecido) {
                    if (sinalFoiReconhecido) {
                        this._idDoSinalParaReconhecer = -1;
                    }
                    return sinalFoiReconhecido;
                }.bind(this));
        },

        setIdDoSinalParaReconhecer: function(id) {
            this._idDoSinalParaReconhecer = id;
            this._algoritmo.setSinalId(id);
        },

        setTipoDoSinal: function(tipoDoSinal) {
            this._tipoDoSinal = tipoDoSinal;
            if (this._ehSinalEstatico(tipoDoSinal)) {
                this._algoritmo = new Signa.reconhecimento.SinalEstatico();
            } else {
                this._algoritmo = new Signa.reconhecimento.SinalDinamico();
            }
        },

        _ehSinalEstatico: function(tipoDoSinal) {
            return tipoDoSinal === 0;
        }
    };

    Signa.reconhecimento.ReconhecedorDeSinaisOnline = ReconhecedorDeSinaisOnline;
})(window, window.Signa);
