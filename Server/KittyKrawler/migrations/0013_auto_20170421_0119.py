# -*- coding: utf-8 -*-
# Generated by Django 1.11 on 2017-04-21 05:19
from __future__ import unicode_literals

from django.db import migrations


class Migration(migrations.Migration):

    dependencies = [
        ('KittyKrawler', '0012_auto_20170421_0115'),
    ]

    operations = [
        migrations.RenameField(
            model_name='gamesave',
            old_name='enkill_counter',
            new_name='kill_counter',
        ),
    ]
