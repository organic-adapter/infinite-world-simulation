<script lang="ts">
import { computed, ref } from "vue";
import { useStore } from "vuex";
import { Hive } from "../../models/Hive";

export default {
  setup() {
    const store = useStore();
    const hive = computed(() => store.getters["hives/focus"] ?? new Hive(""));
    const getWaterTick = ()=>{
        store.dispatch("hives/getWaterTick")
    }
    return {
      getWaterTick,
      hive,
    };
  },
};
</script>
<template>
  <div class="hive-debugger">
    <div>{{ hive.name }}</div>
    <MDBBtn color="primary" v-if="hive.name" @click="getWaterTick"
      >Get Water Tick</MDBBtn
    >
  </div>
</template>
