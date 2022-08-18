<script>
import { ref } from "vue";

export default {
  setup() {
    const navCollapse = ref(false);

    return {
      navCollapse,
    };
  },
};
</script>

<template>
  <div class="app">
    <MDBNavbar expand="md" dark bg="dark" container>
      <MDBNavbarBrand href="#">Hi!</MDBNavbarBrand>
      <MDBNavbarToggler
        @click="navCollapse = !navCollapse"
        target="#navbarSupportedContent"
      ></MDBNavbarToggler>
      <MDBCollapse v-model="navCollapse" id="navbarSupportedContent">
        <MDBNavbarNav class="mb-2 mb-lg-0">
          <MDBNavbarItem to="/"> Main </MDBNavbarItem>
          <MDBNavbarItem href="#/about"> About </MDBNavbarItem>
        </MDBNavbarNav>
      </MDBCollapse>
    </MDBNavbar>
    <MDBContainer fluid>
      <router-view />
    </MDBContainer>
  </div>
</template>

<style lang="scss">
@import "../../../styles/core.scss";
#app {
  font-family: Roboto, Helvetica, Arial, sans-serif;
  color: var(--light-fg);
  background-color: var(--dark-bg);
  height: 100vh;
}
</style>
