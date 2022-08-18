import {
    MDBBtn,
    MDBNavbar,
    MDBNavbarToggler,
    MDBNavbarBrand,
    MDBNavbarNav,
    MDBNavbarItem,
    MDBCollapse,
    MDBDropdown,
    MDBDropdownToggle,
    MDBDropdownMenu,
    MDBDropdownItem
} from 'mdb-vue-ui-kit';
import { MDBContainer } from 'mdb-vue-ui-kit';
export default {
    install(app: any, options: any) {
        app
            .component('MDBBtn', MDBBtn)
            .component('MDBNavbar', MDBNavbar)
            .component('MDBNavbarToggler', MDBNavbarToggler)
            .component('MDBNavbarBrand', MDBNavbarBrand)
            .component('MDBNavbarBrand', MDBNavbarBrand)
            .component('MDBNavbarNav', MDBNavbarNav)
            .component('MDBNavbarItem', MDBNavbarItem)
            .component('MDBCollapse', MDBCollapse)
            .component('MDBDropdown', MDBDropdown)
            .component('MDBDropdownToggle', MDBDropdownToggle)
            .component('MDBDropdownMenu', MDBDropdownMenu)
            .component('MDBDropdownItem', MDBDropdownItem)
            .component('MDBContainer', MDBContainer)
        ;
    }
}