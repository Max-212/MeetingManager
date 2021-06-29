<script>
export default {
    props:
    {
        meeting:
        {
            type: Object,
            default(){
                return {}
            }
        }
    },

    methods:{
        deleteMeeting()
        {
           this.$emit('deleteMeeting', this.meeting.id);
        }
    }
}
</script>

<template>
    <div class="shadow border-0 card m-4 meeting-view">
        <div class="card-body d-flex flex-column justify-content-between">
            <p class="card-text"><small class="text-muted">From:&nbsp;{{meeting.from}}</small></p>
            <p class="card-text"><small class="text-muted">Till:&nbsp;{{meeting.till}}</small></p>
            <p class="card-text">{{meeting.description}}</p>
        </div>
        <hr class="mt-0">
        <h4 class="mb">Participants</h4>
        <div class="card-body">
            <p v-for="participant in meeting.participants" :key="participant.id" class="card-text">
                {{participant.firstName}}&nbsp;{{participant.lastName}}
            </p>
        </div>
        <div class="card-footer d-flex flex-row justify-content-end bg-transparent">
             <router-link class="btn btn-secondary btn m-1" v-bind:to="'/updateMeeting/' + meeting.id" >
                <i class="fa fa-wrench" aria-hidden="true"></i>
            </router-link>
             <button class="btn btn-danger m-1" @click="deleteMeeting">
                <i class="fa fa-trash" aria-hidden="true"></i>
             </button>
        </div>
    </div>
</template>

<style>
    .meeting-view
    {
        width: 280px;
    }
</style>
