<script setup lang="ts">
import { computed, onMounted, ref } from "vue";
import { useStore } from "vuex";
import { Hive } from "../../models/Hive";

const VALIDATION = {
  VALID: "valid",
  INVALID: "invalid",
  PENDING: "validation-pending",
};
const store = useStore();

const openSelectHive = ref(false);
const hiveOptions = computed(() => store.getters["hives/activeHives"]);
const selectedHive = computed(() => store.getters["hives/focus"]);
const hiveName = computed({
  get() {
    const hive = store.getters["hives/focus"] ?? new Hive("");
    return hive.name;
  },
  set(value: string) {
    store.dispatch("hives/setFocusByName", value);
  },
});

const isValidFocus = computed(() => {
  return store.getters["hives/hiveByName"](hiveName.value) !== null;
});
const validationClass = () => {
  if (isValidFocus.value === true) return VALIDATION.VALID;
  if (isValidFocus.value === false) return VALIDATION.INVALID;
  return VALIDATION.PENDING;
};
const selectHive = (hive: Hive) => {
  store.commit("hives/setFocus", hive);
};

onMounted(() => {
  store.dispatch("hives/getActiveHives");
});
</script>
<template>
  <div class="hive-selector">
    <MDBRow tag="form" class="g-3" :class="validationClass()">
      <MDBInput
        inputGroup
        :formOutline="false"
        v-model="hiveName"
        aria-describedby="select-hive-prepend"
        aria-label="Select Hive"
        placeholder="Select Hive"
        class="dark"
      >
        <template #prepend>
          <MDBDropdown v-model="openSelectHive">
            <MDBDropdownToggle
              color="primary"
              @click="openSelectHive = !openSelectHive"
              v-model="selectedHive"
              size="lg"
              >Select Hive</MDBDropdownToggle
            >
            <MDBDropdownMenu>
              <MDBDropdownItem
                v-for="hive in hiveOptions"
                :key="hive.name"
                tag="button"
                @click="selectHive(hive)"
                >{{ hive.name }}</MDBDropdownItem
              >
            </MDBDropdownMenu>
          </MDBDropdown>
        </template>

        <span class="pending input-group-text"
          ><i class="fas fa-circle"></i
        ></span>
        <span class="invalid input-group-text"
          ><i class="fas fa-times-circle"></i
        ></span>
        <span class="valid input-group-text"><i class="fas fa-check"></i></span>
        <span class="info input-group-text"><i class="fas fa-info"></i></span>
      </MDBInput>
    </MDBRow>
  </div>
</template>
<style lang="scss" scoped>
.hive-selector {
  .info {
    color: var(--mdb-info);
    border-color: var(--mdb-info);
  }
}
</style>