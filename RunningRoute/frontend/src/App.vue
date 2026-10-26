<script setup lang="ts">
import { ref } from 'vue'
import { LMap, LTileLayer } from '@vue-leaflet/vue-leaflet'
 
const zoom = ref<number>(13)
const center = ref<[number, number]>([53.3811, -1.4701]) // Sheffield: [latitude, longitude]
</script>
 
<template>
  <div class="map-wrapper">
    <l-map v-model:zoom="zoom" v-model:center="center" :use-global-leaflet="false">
      <l-tile-layer
        url="https://tile.openstreetmap.org/{z}/{x}/{y}.png"
        attribution="&copy; OpenStreetMap contributors"
        layer-type="base"
        name="OpenStreetMap"
      />
    </l-map>
  </div>
</template>
 
<style>
html,
body {
  margin: 0;
}
.map-wrapper {
  height: 100vh;
}
</style>