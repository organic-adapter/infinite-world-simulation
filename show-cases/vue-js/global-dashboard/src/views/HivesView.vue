<script setup lang="ts">
import { ref } from "vue";
import HiveSelector from "../components/hives/HiveSelector.vue";

const components = { HiveSelector };

</script>
<template>
  <div class="hives">
    <h1>Hives</h1>
    <MDBContainer fluid>
      <hive-selector />
    </MDBContainer>
  </div>
</template>
