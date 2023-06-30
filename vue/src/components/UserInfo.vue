<template>
 <div class="user-info">
  <div class="user-img">
    <img src="../images/godzilla-1954-square.jpg" />
  </div>
  <h2>Andrew Clements</h2>
  <h2>Andrew Clements</h2>
  <h2>Andrew Clements</h2>
  <h2>Andrew Clements</h2>
  </div>
</template>

<script>
export default {

}
</script>

<style>

.user-info {
  display: flex;
  flex-direction: column;
  align-items: center;
  text-align: center;
  background: rgba(255, 255, 255, 0.1); 
  backdrop-filter: blur(10px); 
  border: 1px solid rgba(255, 255, 255, 0.2);
  border-radius: 10px;
  padding: 20px;
}

.user-info h2 {
  color: white;
}

</style>