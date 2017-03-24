from rest_framework import serializers
from KittyKrawler.models import GameSave, Leaderboard, Item


class SaveSerializer(serializers.ModelSerializer):
    user_name = serializers.ReadOnlyField(source='user.username')
    item_list = serializers.ListField()

    class Meta:
        model = GameSave
        fields = ('user_name', 'item_list', 'attack', 'defence', 'speed', 'health', 'total_health', 'next_level', 'time')

    def create(self, validated_data):
        return GameSave.objects.create(**validated_data)



class LeaderboardSerializer(serializers.ModelSerializer):
    class Meta:
        model = Leaderboard
        fields = ('user', 'game_save')


class ItemSerializer(serializers.ModelSerializer):
    class Meta:
        model = Item
        fields = ('save_item', 'item_id')