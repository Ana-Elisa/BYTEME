from rest_framework import serializers
from KittyKrawler.models import Save, Leaderboard, Item


class SaveSerializer(serializers.ModelSerializer):
    class Meta:
        model = Save
        fields = ('user', 'attack', 'defence', 'speed', 'health', 'total_health', 'next_level', 'time')


class LeaderboardSerializer(serializers.ModelSerializer):
    class Meta:
        model = Leaderboard
        fields = ('user', 'save')


class ItemSerializer(serializers.ModelSerializer):
    class Meta:
        model = Item
        fields = ('save_item', 'item_id')