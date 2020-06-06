<template>
  <div id="app">
    <Header :updateCenter="setCenter" :submit="getData"></Header>
    <Map v-bind:center="center" :geoData="geoJson"></Map>
  </div>
</template>

<script>
import Header from "@/components/Header.vue";
import Map from "@/components/Map.vue";
export default {
  name: "App",
  components: {
    Header,
    Map
  },
  data() {
    return {
      geoJson: null,
      center: [47.382673, 8.514043]
    };
  },
  methods: {
    setCenter(cords) {
      this.center = cords;
    },
    getData(e) {
      var myHeaders = new Headers();
      myHeaders.append(
        "Authorization",
        "lANKZTQgc8kcFm7RQhYdu668URzk4plMOFwHIgh3BINcSJPtmg"
      );
      myHeaders.append("Content-Type", "application/json");

      var raw = JSON.stringify({
        preference: "fastest",
        language: "de",
        instructions: true,
        geometry: true,
        units: "m",
        attributes: ["detourfactor", "percentage"],
        instructions_format: "html",
        elevation: true,
        options: {
          round_trip: { length: e.distance * 1000, points: 8, seed: 3 }
        },
        coordinates: e.location,
        extra_info: ["steepness", "waytype", "surface"]
      });

      var requestOptions = {
        method: "POST",
        headers: myHeaders,
        body: raw,
        redirect: "follow"
      };

      fetch("http://localhost:7071/api/Directions", requestOptions)
        .then(response => response.text())
        .then(result => (this.geoJson = JSON.parse(result)))
        .catch(error => console.log("error", error));
    }
  }
};
</script>

<style lang="scss">
// Import Bulma's core
@import "~bulma/sass/utilities/_all";

// Set your colors
$primary: #8c67ef;
$primary-invert: findColorInvert($primary);
$twitter: #4099ff;
$twitter-invert: findColorInvert($twitter);

// Setup $colors to use as bulma classes (e.g. 'is-twitter')
$colors: (
  "white": (
    $white,
    $black
  ),
  "black": (
    $black,
    $white
  ),
  "light": (
    $light,
    $light-invert
  ),
  "dark": (
    $dark,
    $dark-invert
  ),
  "primary": (
    $primary,
    $primary-invert
  ),
  "info": (
    $info,
    $info-invert
  ),
  "success": (
    $success,
    $success-invert
  ),
  "warning": (
    $warning,
    $warning-invert
  ),
  "danger": (
    $danger,
    $danger-invert
  ),
  "twitter": (
    $twitter,
    $twitter-invert
  )
);

// Links
$link: $primary;
$link-invert: $primary-invert;
$link-focus-border: $primary;

// Import Bulma and Buefy styles
@import "~bulma";
@import "~buefy/src/scss/buefy";
</style>
