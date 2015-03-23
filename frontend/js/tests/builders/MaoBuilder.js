var DedoBuilder = require('./DedoBuilder.js');

function MaoBuilder() {
    this._direcao = [0.0, 0.0, 0.0];
    this._vetorNormalDaPalma = [0.0, 0.0, 0.0];
    this._posicaoDaPalma = [0.0, 0.0, 0.0];
    this._velocidadeDaPalma = [0.0, 0.0, 0.0];
    this._raioDaEsfera = 0;
    this._pitch = function(){ return 0; };
    this._yaw = function(){ return 0; };
    this._roll = function(){ return 0; };
    
    this._dedos = [
        new DedoBuilder().construir(),
        new DedoBuilder().construir(),
        new DedoBuilder().construir(),
        new DedoBuilder().construir(),
        new DedoBuilder().construir()
    ];
}
MaoBuilder.prototype = {
    _direcao: undefined,
    _vetorNormalDaPalma: undefined,
    _dedos: undefined,
    _posicaoDaPalma: undefined,
    _velocidadeDaPalma: undefined,
    _raioDaEsfera: 0,
    _pitch: undefined,
    _yaw: undefined,
    _roll: undefined,

    comDirecao: function(direcao) {
        this._direcao = direcao;
        return this;
    },

    comVetorNormalDaPalma: function(vetorNormalDaPalma) {
        this._vetorNormalDaPalma = vetorNormalDaPalma;
        return this;
    },

    comDedos: function(dedos) {
        this._dedos = dedos;
        return this;
    },

    construir: function() {
        return {
            palmNormal: this._vetorNormalDaPalma,
            direction: this._direcao,
            palmVelocity: this._velocidadeDaPalma,
            palmPosition: this._posicaoDaPalma,
            stabilizedPalmPosition: this._posicaoDaPalma,
            sphereRadius: this._raioDaEsfera,
            pitch: this._pitch,
            roll: this._roll,
            yaw: this._yaw,
            fingers: this._dedos
        };
    }
};

module.exports = MaoBuilder;
