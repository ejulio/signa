function RiggedHand(leapMotionController, parameters)
{
    this._leapMotionController = leapMotionController;
    this._parameters = parameters;

    parameters.offset = parameters.offset || new THREE.Vector3(0, -10, 0);
    parameters.scale = parameters.scale || 1;
    parameters.positionScale = parameters.positionScale || 1;

    this._projector = new THREE.Projector();

    this._spareMeshes = {
        left: [],
        right: []
    };

    this._zeroVector = new THREE.Vector3(0, 0, 0);

    this.onFrame = this.onFrame.bind(this);

    this._init();
}

RiggedHand.prototype = {
    _leapMotionController: undefined,
    _parameters: undefined,

    _init: function()
    {
        this._leapMotionController.use('handHold');
        this._leapMotionController.use('handEntry');
        this._leapMotionController.use('versionCheck', {
          requiredProtocolVersion: 6
        });

        this._leapMotionController.on('handFound', this._addMesh.bind(this));
        this._leapMotionController.on('handLost', this._removeMesh.bind(this));

        dots = {};
        basicDotMesh = new THREE.Mesh(new THREE.IcosahedronGeometry(.3, 1), new THREE.MeshNormalMaterial());
        var parameters = this._parameters;
        parameters.positionDots = function(leapHand, handMesh, offset) {
            var i, leapFinger, point, _i, _len, _ref, _results;
            if (!parameters.dotsMode) {
                return;
            }
            _ref = leapHand.fingers;
            _results = [];
            for (i = _i = 0, _len = _ref.length; _i < _len; i = ++_i) {
                leapFinger = _ref[i];
                _results.push((function() {
                    var _j, _len1, _ref1, _results1;
                    _ref1 = ['mcp', 'pip', 'dip', 'tip'];
                    _results1 = [];
                    for (_j = 0, _len1 = _ref1.length; _j < _len1; _j++) {
                        point = _ref1[_j];
                        if (!dots["" + point + "-" + i]) {
                            dots["" + point + "-" + i] = basicDotMesh.clone();
                            parameters.parent.add(dots["" + point + "-" + i]);
                        }
                        _results1.push(handMesh.scenePosition(leapFinger["" + point + "Position"], dots["" + point + "-" + i].position, offset));
                    }
                  return _results1;
                })());
            }
            return _results;
        }
    },

    _addMesh: function(leapHand)
    {
        var handMesh, palm, rigFinger, _i, _len, _ref;
        handMesh = this._getMesh(leapHand);
        console.log(handMesh.uuid);
        this._parameters.parent.add(handMesh);
        leapHand.data('riggedHand.mesh' + this._parameters.sceneid, handMesh);
        palm = handMesh.children[0];
        handMesh.leapScale = (new THREE.Vector3).subVectors((new THREE.Vector3).fromArray(leapHand.fingers[2].pipPosition), (new THREE.Vector3).fromArray(leapHand.fingers[2].mcpPosition)).length() / handMesh.fingers[2].position.length() / this._parameters.scale;
        palm.worldUp = new THREE.Vector3;
        palm.positionLeap = new THREE.Vector3;
        _ref = handMesh.fingers;
        for (_i = 0, _len = _ref.length; _i < _len; _i++) {
            rigFinger = _ref[_i];
            rigFinger.pip = rigFinger.children[0];
            rigFinger.dip = rigFinger.pip.children[0];
            rigFinger.tip = rigFinger.dip.children[0];
            rigFinger.worldQuaternion = new THREE.Quaternion;
            rigFinger.pip.worldQuaternion = new THREE.Quaternion;
            rigFinger.dip.worldQuaternion = new THREE.Quaternion;
            rigFinger.worldAxis = new THREE.Vector3;
            rigFinger.pip.worldAxis = new THREE.Vector3;
            rigFinger.dip.worldAxis = new THREE.Vector3;
            rigFinger.worldDirection = new THREE.Vector3;
            rigFinger.pip.worldDirection = new THREE.Vector3;
            rigFinger.dip.worldDirection = new THREE.Vector3;
            rigFinger.worldUp = new THREE.Vector3;
            rigFinger.pip.worldUp = new THREE.Vector3;
            rigFinger.dip.worldUp = new THREE.Vector3;
            rigFinger.positionLeap = new THREE.Vector3;
            rigFinger.pip.positionLeap = new THREE.Vector3;
            rigFinger.dip.positionLeap = new THREE.Vector3;
            rigFinger.tip.positionLeap = new THREE.Vector3;
        }
        palm.worldDirection = new THREE.Vector3;
        palm.worldQuaternion = handMesh.quaternion;
        if (this._parameters.boneLabels) {
            handMesh.children[0].traverse(function(bone) {
                return document.body.appendChild(handMesh.boneLabels[bone.id]);
            });
        }
        return this._leapMotionController.emit('riggedHand.meshAdded', handMesh, leapHand);
    },

    _getMesh: function(leapHand) {
        var JSON, handMesh, meshes;
        meshes = this._spareMeshes[leapHand.type];
        if (meshes.length > 0) {
            handMesh = meshes.pop();
        } else {
            JSON = rigs[leapHand.type];
            handMesh = this._createMesh(JSON);
        }
        return handMesh;
    },

    _createMesh: function(JSON) {
        var data, handMesh, i;
        data = (new THREE.JSONLoader).parse(JSON);
        data.materials[0].skinning = true;
        data.materials[0].transparent = true;
        data.materials[0].opacity = 0.7;
        data.materials[0].emissive.setHex(0x888888);
        data.materials[0].vertexColors = THREE.VertexColors;
        data.materials[0].depthTest = true;
        _extend(data.materials[0], this._parameters.materialOptions);
        _extend(data.geometry, this._parameters.geometryOptions);
        handMesh = new THREE.SkinnedMesh(data.geometry, data.materials[0]);
        handMesh.scale.multiplyScalar(this._parameters.scale);
        handMesh.positionRaw = new THREE.Vector3;
        handMesh.fingers = handMesh.children[0].children;
        handMesh.castShadow = true;
        handMesh.bonesBySkinIndex = {};
        i = 0;
        handMesh.children[0].traverse(function(bone) {
            bone.skinIndex = i;
            handMesh.bonesBySkinIndex[i] = bone;
            return i++;
        });
        handMesh.boneLabels = {};
        if (this._parameters.boneLabels) {
            handMesh.traverse(function(bone) {
                var attribute, label, value, _base, _name, _ref, _results;
                label = (_base = handMesh.boneLabels)[_name = bone.id] || (_base[_name] = document.createElement('div'));
                label.style.position = 'absolute';
                label.style.zIndex = '10';
                label.style.color = 'white';
                label.style.fontSize = '20px';
                label.style.textShadow = '0px 0px 3px black';
                label.style.fontFamily = 'helvetica';
                label.style.textAlign = 'center';
                _ref = this._parameters.labelAttributes;
                _results = [];
                for (attribute in _ref) {
                    value = _ref[attribute];
                    _results.push(label.setAttribute(attribute, value));
                }
                return _results;
            });
        }
        handMesh.screenPosition = function(position) {
            var camera, height, screenPosition, width;
            camera = this._parameters.camera;
            console.assert(camera instanceof THREE.Camera, "screenPosition expects camera, got", camera);
            width = this._parameters.renderer.domElement.width;
            height = this._parameters.renderer.domElement.height;
            console.assert(width && height);
            screenPosition = new THREE.Vector3();
            if (position instanceof THREE.Vector3) {
                screenPosition.fromLeap(position.toArray(), this.leapScale);
            } else {
                screenPosition.fromLeap(position, this.leapScale).sub(this.positionRaw).add(this.position);
            }
            screenPosition = this._projector.projectVector(screenPosition, camera);
            screenPosition.x = (screenPosition.x * width / 2) + width / 2;
            screenPosition.y = (screenPosition.y * height / 2) + height / 2;
            console.assert(!isNaN(screenPosition.x) && !isNaN(screenPosition.x), 'x/y screen position invalid');
            return screenPosition;
        }.bind(this);
        handMesh.scenePosition = function(leapPosition, scenePosition, offset) {
            return scenePosition.fromLeap(leapPosition, handMesh.leapScale, offset).sub(handMesh.positionRaw).add(handMesh.position);
        };
        return handMesh;
    },

    _removeMesh: function(leapHand)
    {
        var handMesh = leapHand.data('riggedHand.mesh' + this._parameters.sceneid);
        leapHand.data('riggedHand.mesh' + this._parameters.sceneid, null);
        this._parameters.parent.remove(handMesh);
        this._spareMeshes[leapHand.type].push(handMesh);
        if (this._parameters.boneLabels) {
            handMesh.children[0].traverse(function(bone) {
              return document.body.removeChild(handMesh.boneLabels[bone.id]);
            });
        }

        this._leapMotionController.emit('riggedHand.meshRemoved', handMesh, leapHand);

        if (this._parameters.renderFn) {
            return this._parameters.renderFn();
        }
    },

    onFrame: function(frame)
    {
        var boneColors, face, faceIndices, geometry, handMesh, hue, i, j, leapHand, lightness, mcp, offset, palm, saturation, weights, xBoneHSL, yBoneHSL, _base, _i, _j, _k, _l, _len, _len1, _len2, _len3, _name, _name1, _ref, _ref1, _ref2, _ref3;
        if (this._parameters.stats) {
          this._parameters.stats.begin();
        }
        _ref = frame.hands;
        for (_i = 0, _len = _ref.length; _i < _len; _i++) {
          leapHand = _ref[_i];
          leapHand.fingers = _sortBy(leapHand.fingers, function(finger) {
            return finger.id;
          });
          handMesh = leapHand.data('riggedHand.mesh' + this._parameters.sceneid);
          palm = handMesh.children[0];
          palm.positionLeap.fromArray(leapHand.palmPosition);
          _ref1 = palm.children;
          for (i = _j = 0, _len1 = _ref1.length; _j < _len1; i = ++_j) {
            mcp = _ref1[i];
            mcp.positionLeap.fromArray(leapHand.fingers[i].mcpPosition);
            mcp.pip.positionLeap.fromArray(leapHand.fingers[i].pipPosition);
            mcp.dip.positionLeap.fromArray(leapHand.fingers[i].dipPosition);
            mcp.tip.positionLeap.fromArray(leapHand.fingers[i].tipPosition);
          }
          palm.worldDirection.fromArray(leapHand.direction);
          palm.up.fromArray(leapHand.palmNormal).multiplyScalar(-1);
          palm.worldUp.fromArray(leapHand.palmNormal).multiplyScalar(-1);
          offset = typeof this._parameters.offset === 'function' ? this._parameters.offset(leapHand) : this._parameters.offset;
          handMesh.positionRaw.fromLeap(leapHand.palmPosition, handMesh.leapScale, offset);
          handMesh.position.copy(handMesh.positionRaw).multiplyScalar(this._parameters.positionScale);
          handMesh.matrix.lookAt(palm.worldDirection, this._zeroVector, palm.up);
          palm.worldQuaternion.setFromRotationMatrix(handMesh.matrix);
          _ref2 = palm.children;
          for (_k = 0, _len2 = _ref2.length; _k < _len2; _k++) {
            mcp = _ref2[_k];
            mcp.traverse(function(bone) {
              if (bone.children[0]) {
                bone.worldDirection.subVectors(bone.children[0].positionLeap, bone.positionLeap).normalize();
                return bone.positionFromWorld(bone.children[0].positionLeap, bone.positionLeap);
              }
            });
          }
          this._parameters.positionDots(leapHand, handMesh, offset);
          if (this._parameters.boneLabels) {
            palm.traverse(function(bone) {
              var element, screenPosition;
              if (element = handMesh.boneLabels[bone.id]) {
                screenPosition = handMesh.screenPosition(bone.positionLeap, this._parameters.camera);
                element.style.left = "" + screenPosition.x + "px";
                element.style.bottom = "" + screenPosition.y + "px";
                return element.innerHTML = this._parameters.boneLabels(bone, leapHand) || '';
              }
            });
          }
          if (this._parameters.boneColors) {
            geometry = handMesh.geometry;
            boneColors = {};
            i = 0;
            while (i < geometry.vertices.length) {
              boneColors[_name = geometry.skinIndices[i].x] || (boneColors[_name] = this._parameters.boneColors(handMesh.bonesBySkinIndex[geometry.skinIndices[i].x], leapHand) || {
                hue: 0,
                saturation: 0
              });
              boneColors[_name1 = geometry.skinIndices[i].y] || (boneColors[_name1] = this._parameters.boneColors(handMesh.bonesBySkinIndex[geometry.skinIndices[i].y], leapHand) || {
                hue: 0,
                saturation: 0
              });
              xBoneHSL = boneColors[geometry.skinIndices[i].x];
              yBoneHSL = boneColors[geometry.skinIndices[i].y];
              weights = geometry.skinWeights[i];
              hue = xBoneHSL.hue || yBoneHSL.hue;
              lightness = xBoneHSL.lightness || yBoneHSL.lightness || 0.5;
              saturation = xBoneHSL.saturation * weights.x + yBoneHSL.saturation * weights.y;
              (_base = geometry.colors)[i] || (_base[i] = new THREE.Color());
              geometry.colors[i].setHSL(hue, saturation, lightness);
              i++;
            }
            geometry.colorsNeedUpdate = true;
            faceIndices = 'abc';
            _ref3 = geometry.faces;
            for (_l = 0, _len3 = _ref3.length; _l < _len3; _l++) {
              face = _ref3[_l];
              j = 0;
              while (j < 3) {
                face.vertexColors[j] = geometry.colors[face[faceIndices[j]]];
                j++;
              }
            }
          }
        }
        if (this._parameters.renderFn) {
          this._parameters.renderFn();
        }
        if (this._parameters.stats) {
          return this._parameters.stats.end();
        }
    }
};
