<script>
    
    export default {
        props:
        {
            participant:
            {
                type: Object,
                default(){
                    return {}
                }
            }
        },
        methods:
        {
            removeParticipant()
            {
                this.$emit('removeParticipant', this.participant);
            }
        }
    }

</script>

<template>
    <div class="rounded d-flex justify-content-between align-items-center flex-wrap mb-1 p-2 w-100 border">
        <h5 class="fw-bold text-secondary">{{participant.email}}</h5>
        <button class="btn btn-danger mx-3 btn" @click="removeParticipant">
                <i class="fa fa-trash" aria-hidden="true"></i>
        </button>
    </div>
</template>
