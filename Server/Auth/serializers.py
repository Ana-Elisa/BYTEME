from django.contrib.auth.models import User
from rest_framework import serializers
from KittyKrawler.models import GameSave


class UserSerializer(serializers.ModelSerializer):
    game_save = serializers.PrimaryKeyRelatedField(many=False, read_only=True)

    class Meta:
        model = User
        fields = ('username', 'email', 'password', 'game_save')
        write_only_fields = ('password',)

    def create(self, validated_data):
        user = User.objects.create_user(**validated_data)
        return user