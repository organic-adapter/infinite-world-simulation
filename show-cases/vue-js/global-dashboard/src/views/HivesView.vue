<script setup lang="ts">
import { ref } from "vue";
import HiveDebugger from "../components/hives/HiveDebugger.vue";
import HiveSelector from "../components/hives/HiveSelector.vue";

const components = { HiveDebugger, HiveSelector };


</script>
<template>
  <div class="hives">
    <h1>Hives</h1>
    <MDBContainer fluid>
      <hive-selector />
      <hive-debugger />
    </MDBContainer>
  </div>
</template>
