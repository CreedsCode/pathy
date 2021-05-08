<template>
  <l-map :zoom="zoom" :center="center">
    <l-tile-layer :url="url"></l-tile-layer>
    <l-geo-json :geojson="geoData"></l-geo-json>
  </l-map>
  <!-- <div class="" style="height: 15%">
                <span>Center: {{ center }}</span>
                <span>Zoom: {{ zoom }}</span>
                <span>Bounds: {{ bounds }}</span>
            </div> -->
</template>

<script>
import { LMap, LTileLayer, LGeoJson } from "vue2-leaflet";

export default {
  components: {
    LMap,
    LTileLayer,
    LGeoJson
  },
  props: {
    geoData: { type: Object },
    center: { type: Array }
  },
  data() {
    return {
      url: "https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png",
      zoom: 14,
      bounds: null
    };
  },
  // watch: {
  //   geoData: function(newVal, oldVal){

  //   }
  // },
  methods: {}
};
</script>

<style lang="css" scoped>
/* Element | http://localhost:8080/ */

.vue2leaflet-map {
  /* position: relative; */
  position: fixed;
}
</style>
