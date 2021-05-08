<template>
  <section class="hero is-primary">
    <div class="hero-body is-mobile">
      <h1 class="title">
        Pathy
      </h1>
      <h2 class="subtitle">
        Just go for a walk.
      </h2>
      <div class="subtitle level">
        <div class="level-left">
          <div class="field has-addons level-item">
            <p class="control">
              <a @click="getGeoLocation" class="button is-secondary">
                <b-icon icon="compass"></b-icon>
              </a>
            </p>
            <b-autocomplete
              v-model="geoCodeInput"
              :data="geoCodeArray"
              placeholder="start&end location"
              icon="search-location"
              clearable
              @select="option => (selected = option)"
            >
              <template slot="empty">No results found</template>
            </b-autocomplete>

            <p class="control has-icons-right">
              <input
                type="text"
                v-model="query.distance"
                placeholder="distance in km"
                class="input"
              />
              <b-icon class="is-right" icon="route"></b-icon>
            </p>
            <div class="control">
              <button @click="getRoute" class="button is-success">
                Get route
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<script>
// add distance with min ammount of walk needed for a healthy life in a day,
// so if they press on gps they get auto {getroute}
// on location and get route re-center!!
import _ from "lodash";

export default {
  props: {
    submit: { type: Function },
    updateCenter: { type: Function }
  },
  data() {
    return {
      query: {
        location: [[47.382673, 8.514043]],
        distance: 7
      },
      geoCodeInput: "",
      geoCodeArray: []
    };
  },
  methods: {
    getRoute() {
      this.fetchGeoCodeData();
    },
    getGeoLocation() {
      navigator.geolocation.getCurrentPosition(this.setLocationFromNavigator);
    },
    setLocationFromNavigator(location) {
      var coords = location.coords;
      this.query.location = [[coords.longitude, coords.latitude]];
      this.updateCenter([coords.latitude, coords.longitude]);
      this.getGeoLocation = [coords.longitude, coords.latitude];
      this.fetchGeoCodeData();
    },
    fetchGeoAutoCompleteData() {
      var requestOptions = {
        method: "GET",
        redirect: "follow"
      };

      fetch(
        "/api/Autocomplete?text=" + this.geoCodeInput,
        requestOptions
      )
        .then(response => response.text())
        .then(result => this.setGeoCodeArray(JSON.parse(result)))
        .catch(error => console.log("error", error));
    },
    fetchGeoCodeData() {
      var requestOptions = {
        method: "GET",
        redirect: "follow"
      };

      fetch(
        "/api/Search?text=" + this.geoCodeInput,
        requestOptions
      )
        .then(response => response.text())
        .then(result => this.setLocation(JSON.parse(result)))
        .catch(error => console.log("BUG DUDE", error));
    },
    setLocation(result) {
      for (let i = 0; i < result.features.length; i++) {
        const element = result.features[i];

        if (element.properties.label === this.geoCodeInput) {
          this.query.location[0] = element.geometry.coordinates;
          this.updateCenter([
            element.geometry.coordinates[1],
            element.geometry.coordinates[0]
          ]);
          console.log(element);
          this.submit(this.query);
          break;
        }
      }
      // iterate through each element and find that lable that does with the input match
      // and set those location data to the query
    },
    setGeoCodeArray(result) {
      var features = result.features;
      var labels = [];
      features.forEach(feature => {
        labels.push(feature.properties.label);
      });
      this.geoCodeArray = labels;
    },
    setQueryData() {
      this.query.location = this.geoCodeArray[0];
    }
  },
  created() {
    this.debouncedGetGeoCode = _.debounce(this.fetchGeoAutoCompleteData, 500);
  },
  watch: {
    geoCodeInput() {
      this.debouncedGetGeoCode();
    }
  },
  computed: {}
};
</script>

<style>
span {
  color: #4a4a4a;
}
div .hero-body {
  padding-top: 1rem;
  padding-bottom: 1.2rem;
}
</style>
