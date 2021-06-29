<template>
  <Header></Header>
  <div class="main-wrapper">
    <router-view />
  </div>
  
</template>

<script>
  import Header from "./components/Header";
  

  export default {
        name: 'app',
        components: {
          Header,
        }
    }
</script>

<style>
  #app {
    font-family: Avenir, Helvetica, Arial, sans-serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    text-align: center;
  }

  .main-wrapper{
     margin-top: 100px;
     padding: 1%;
  }

  body{
    background: rgb(250, 250, 250);
  }
</style>
