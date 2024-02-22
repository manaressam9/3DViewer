
//import { greet } from '../lib/three/mod1.js';

//const message = greet('John');
//console.log(message); // Output: Hello, John!

import * as THREE from '../lib/three/build/three.module.js';
import Stats from '../lib/three/examples/jsm/libs/stats.module.js';
import { GLTFLoader } from '../lib/three/examples/jsm/loaders/GLTFLoader.js';
import { RoomEnvironment } from '../lib/three/examples/jsm/environments/RoomEnvironment.js';
import { ArcballControls } from '../lib/three/examples/jsm/controls/ArcballControls.js';
console.log("111111111")
let mixer;

//clock is used for models that has animationns
/*const clock = new THREE.Clock();*/
const container = document.getElementById('threedCont');
let containerwidth = container.offsetWidth;
let containerheight = container.offsetHeight;
//create new instance of stats panel and add it to container
const stats = new Stats();
container.appendChild(stats.dom);

const renderer = window.renderer = new THREE.WebGLRenderer({ antialias: true });
renderer.setPixelRatio(window.devicePixelRatio);
renderer.setSize(container.offsetWidth, container.offsetHeight);
container.appendChild(renderer.domElement);
debugger;
const pmremGenerator = new THREE.PMREMGenerator(renderer);
pmremGenerator.compileEquirectangularShader();
//const neutralEnv = pmremGenerator.fromScene( new RoomEnvironment()).texture
//create new scene
const scene = new THREE.Scene();
scene.background = new THREE.Color(0xffffff);
scene.environment = pmremGenerator.fromScene(new RoomEnvironment(renderer), 0.04).texture;
debugger;

const camera = new THREE.PerspectiveCamera(40, containerwidth / containerheight, 1, 1000);
camera.position.set(5, 2, 8); // 5,2,8 x,y,z
scene.add(camera);
debugger;

const controls = new ArcballControls(camera, renderer.domElement, scene);
controls.target.set(0, 0, 0); //0,0.5,0
controls.update();
/*controls.enablePan = true;*/
controls.enableDamping = true;
debugger;

// const dracoLoader = new DRACOLoader();
// dracoLoader.setDecoderPath('three/examples/jsm/libs/draco/gltf/');
const loader = new GLTFLoader();
// loader.setDRACOLoader( dracoLoader );
// loader.setMeshoptDecoder(MeshoptDecoder)

debugger;
loader.load('/threedModels/model1.glb', function (gltf) {
	debugger;
	console.log(gltf);
	const model = gltf.scene;
	console.log(model)
	model.position.set(0, 0, 0);
	model.scale.set(10, 10, 10);
	scene.add(model);
	debugger;
	if (gltf.animations.length > 0) {
		mixer = new THREE.AnimationMixer(model);
		mixer.clipAction(gltf.animations[0]).play();
	}

	animate();



	//animate();

}, undefined, function (e) {

	console.error(e);

});



window.onresize = function () {

	camera.aspect = containerwidth / containerheight;
	camera.updateProjectionMatrix();

	renderer.setSize(containerwidth, containerheight);

};

function animate() {

	requestAnimationFrame(animate);

	/* //used when the model contains animations
	const delta = clock.getDelta();
	
	mixer.update( delta );*/

	controls.update();

	stats.update();

	renderer.render(scene, camera);

}
