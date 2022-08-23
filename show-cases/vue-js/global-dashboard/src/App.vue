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
      <MDBNavbarBrand href="/">Aether Essence World</MDBNavbarBrand>
      <MDBNavbarToggler
        @click="navCollapse = !navCollapse"
        target="#navbarSupportedContent"
      ></MDBNavbarToggler>
      <MDBCollapse v-model="navCollapse" id="navbarSupportedContent">
        <MDBNavbarNav class="mb-2 mb-lg-0">
          <MDBNavbarItem to="/"> Main </MDBNavbarItem>
          <MDBNavbarItem to="/hives"> Hives </MDBNavbarItem>
          <MDBNavbarItem href="#/about"> About </MDBNavbarItem>
        </MDBNavbarNav>
      </MDBCollapse>
    </MDBNavbar>
    <MDBContainer fluid>
      <router-view />
    </MDBContainer>
    <MDBFooter bg="dark" :text="['center', 'lg-start']" class="app-footer">
      <div>&copy; 2022</div>
    </MDBFooter>
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
.app-footer {
  position: fixed;
  bottom: 0;
  width: 100vw;
}

.valid {
  border-color: var(--success-fg);
  .valid {
    display: initial;
    color: var(--success-fg);
    border: 1px;
    border-color: var(--success-fg);
    background-color: var(--success-bg);
  }
  .invalid {
    display: none;
  }
  .pending {
    display: none;
  }
}

.invalid {
  border-color: var(--failure-fg);
  .valid {
    display: none;
  }
  .invalid {
    display: initial;
    color: var(--failure-fg);
    border: 1px;
    border-color: var(--failure-fg);
    background-color: var(--failure-bg);
  }
  .pending {
    display: none;
  }
}
.form-control {
  &.dark {
    color: var(--mdb-light);
    background-color: var(--mdb-dark);
  }
}
</style>
