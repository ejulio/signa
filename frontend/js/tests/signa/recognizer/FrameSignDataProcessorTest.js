var FrameBuilder = require('../../builders/FrameBuilder.js'),
    MaoBuilder = require('../../builders/MaoBuilder.js'),
    DedoBuilder = require('../../builders/DedoBuilder.js');

global.THREE = require('three');
require('../../../src/signa/Signa.js');
require('../../../src/signa/recognizer/FrameSignDataProcessor.js');

var FrameSignDataProcessor = global.Signa.recognizer.FrameSignDataProcessor;

describe('FrameSignDataProcessor', function()
{
    var DIRECAO_MAO_ESQUERDA = [1, 2, 3],
        VETOR_NORMAL_PALMA_MAO_ESQUERDA = [0, 0, 1],
        DIRECAO_MAO_DIREITA = [4, 5, 6],
        VETOR_NORMAL_PALMA_MAO_DIREITA = [1, 1, 0];

    var frameSignDataProcessor;
    beforeEach(function()
    {
        frameSignDataProcessor = new FrameSignDataProcessor();
    });

    it('is getting sign data from a frame with left hand', function()
    {
        var maoEsquerda = dadaUmaMaoEsquerda();
        var frame = dadoUmFrameComMaos(maoEsquerda, null);

        var frameDaAmostra = frameSignDataProcessor.extrairFrameDaAmostra(frame);

        expect(frameDaAmostra.maoDireita).toBeNull();
        deveRetornarUmFrameDaAmostraComDadosDoLeapFrame(frameDaAmostra.maoEsquerda, maoEsquerda);
    });

    it('is getting sign data from a frame with right hand', function()
    {
        var maoDireita = dadaUmaMaoDireita();
        var frame = dadoUmFrameComMaos(null, maoDireita);

        var frameDaAmostra = frameSignDataProcessor.extrairFrameDaAmostra(frame);

        expect(frameDaAmostra.maoEsquerda).toBeNull();
        deveRetornarUmFrameDaAmostraComDadosDoLeapFrame(frameDaAmostra.maoDireita, maoDireita);
    });

    it('is getting sign data from a frame with two hands', function()
    {
        var maoEsquerda = dadaUmaMaoEsquerda();
        var maoDireita = dadaUmaMaoDireita();
        var frame = dadoUmFrameComMaos(maoEsquerda, maoDireita);

        var frameDaAmostra = frameSignDataProcessor.extrairFrameDaAmostra(frame);

        deveRetornarUmFrameDaAmostraComDadosDoLeapFrame(frameDaAmostra.maoEsquerda, maoEsquerda);
        deveRetornarUmFrameDaAmostraComDadosDoLeapFrame(frameDaAmostra.maoDireita, maoDireita);
    });

    function dadaUmaMaoEsquerda()
    {
        var dedos = [
            DedoBuilder.dedao(),
            DedoBuilder.indicador(),
            DedoBuilder.meio(),
            DedoBuilder.anelar(),
            DedoBuilder.mindinho()
        ];

        return new MaoBuilder()
            .comDirecao(DIRECAO_MAO_ESQUERDA)
            .comVetorNormalDaPalma(VETOR_NORMAL_PALMA_MAO_ESQUERDA)
            .comDedos(dedos)
            .construir();
    }

    function dadaUmaMaoDireita()
    {
        var dedos = [
            DedoBuilder.dedao(),
            DedoBuilder.indicador(),
            DedoBuilder.meio(),
            DedoBuilder.anelar(),
            DedoBuilder.mindinho()
        ];

        return new MaoBuilder()
            .comDirecao(DIRECAO_MAO_DIREITA)
            .comVetorNormalDaPalma(VETOR_NORMAL_PALMA_MAO_DIREITA)
            .comDedos(dedos)
            .construir();
    }

    function dadoUmFrameComMaos(maoEsquerda, maoDireita)
    {
        return new FrameBuilder()
            .comMaoEsquerda(maoEsquerda)
            .comMaoDireita(maoDireita)
            .construir();
    }

    function deveRetornarUmFrameDaAmostraComDadosDoLeapFrame(frameDaAmostra, mao)
    {
        expect(frameDaAmostra).not.toBeNull();
        expect(frameDaAmostra.vetorNormalDaPalma).toBe(mao.palmNormal);
        expect(frameDaAmostra.direcao).toBe(mao.direction);
        
        var dedos = frameDaAmostra.dedos;        
        for (var i = 0; i < dedos.length; i++)
        {
            expect(dedos[i].tipo).toBe(mao.fingers[i].type);
            expect(dedos[i].direcao).toBe(mao.fingers[i].direction);
        }
    }
});
