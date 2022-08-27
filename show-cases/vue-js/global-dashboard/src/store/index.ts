import { createStore } from 'vuex'
import hives from './hives';
import actions from './actions';
import state from './state';

export default createStore({
  state,
  getters: {
  },
  mutations: {
  },
  actions,
  modules: {
    hives
  }
})
